using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpLink
{
    class Class1
    {
        const int NUMITEMS = 10;

        // Also note what the actual array size is.( NUMITEMS SPECIFIES Upper Bound of Array )
        // To summarize then:
        //	1) Array size = 11
        //	2) We use indexes 1 thru 10
        //	3) Index 0 is not used at all
        const int ACTUAL_ARRAY_SIZE = NUMITEMS + 1;

        OPCAutomation.OPCServer AnOPCServer;
        OPCAutomation.OPCServer ConnectedOPCServer;
        OPCAutomation.OPCGroup ConnectedGroup;

        // OPC Item related data
        string[] OPCItemIDs = new string[NUMITEMS + 1];
        int[] ClientHandles = new int[NUMITEMS + 1];
        System.Array ItemServerHandles;

        // Arrays are used to provide iterative access to sets of controls
        object[] OPCItemName = new object[NUMITEMS + 1];
        object[] OPCItemValue = new object[NUMITEMS + 1];
        object[] OPCItemValueToWrite = new object[NUMITEMS + 1];
        object[] OPCItemWriteButton = new object[NUMITEMS + 1];
        object[] OPCItemActiveState = new object[NUMITEMS + 1];
        object[] OPCItemSyncReadButton = new object[NUMITEMS + 1];
        object[] OPCItemQuality = new object[NUMITEMS + 1];
        int[] OPCItemIsArray = new int[NUMITEMS + 1];

        private void OPCAddItems_Click(System.Object sender, System.EventArgs e)
        {
            // Test to see if the OPC Group object is currently available
            if (!(ConnectedGroup == null))
            {
                try
                {
                    int ItemCount = System.Convert.ToInt32(NUMITEMS);

                    // Array for potential error returns.  This example doesn't
                    // check them but yours should ultimately.
                    System.Array AddItemServerErrors = default(System.Array);

                    // Load the request OPC Item names and build the ClientHandles list
                    for (short i = 1; i <= NUMITEMS; i++)
                    {
                        // Load the name of then item to be added to this group.  You can add
                        // as many items as you want to the group in a single call by building these
                        // arrays as needed.
                        OPCItemIDs(i) = OPCItemName(i).Text;

                        // ASSume all aren't an array. If it is, this holds size and is set in
                        // Data change event.
                        OPCItemIsArray(i) = 0;

                        // The client handles are given to the OPC Server for each item you intend
                        // to add to the group.  The OPC Server will uses these client handles
                        // by returning them to you in the 'DataChange' event.  You can use the
                        // client handles as a key to linking each valued returned from the Server
                        // back to some element in your application.  In this example we are simply
                        // placing the Index number of each control that will be used to display
                        // data for the item.  In your application the ClientHandle value you use
                        // can by whatever you need to best fit your program.  You will see how
                        // these client handles are used in the 'DataChange' event handler.
                        ClientHandles[i] = i;

                        // Make the Items active start control Active, for the demo I want all items to start active
                        // Your application may need to start the items as inactive.
                        OPCItemActiveState(i).CheckState = System.Windows.Forms.CheckState.Checked;
                    }

                    // Establish a connection to the OPC item interface of the connected group
                    //                OPCItemCollection = ConnectedGroup.OPCItems

                    // Setting the '.DefaultIsActive' property forces all items we are about to
                    // add to the group to be added in an active state.  If you want to add them
                    // all as inactive simply set this property false, you can always make the
                    // items active later as needed using each item's own active state property.
                    // One key distinction to note, the active state of an item is independent
                    // from the group active state.  If a group is active but the item is
                    // inactive no data will be received for the item.  Also changing the
                    // state of the group will not change the state of an item.
                    ConnectedGroup.OPCItems.DefaultIsActive = true;

                    // Atempt to add the items,  some may fail so the ItemServerErrors will need
                    // to be check on completion of the call.  We are adding all item using the
                    // default data type of VT_EMPTY and letting the server pick the appropriate
                    // data type.  The ItemServerHandles is an array that the OPC Server will
                    // return to your application.  This array like your own ClientHandles array
                    // is used by the server to allow you to reference individual items in an OPC
                    // group.  When you need to perform an action on a single OPC item you will
                    // need to use the ItemServerHandles for that item.  With this said you need to
                    // maintain the ItemServerHandles array for use throughout your application.
                    // Use of the ItemServerHandles will be demonstrated in other subroutines in
                    // this example program.
                    ConnectedGroup.OPCItems.AddItems(ItemCount, OPCItemIDs, ClientHandles, ItemServerHandles, AddItemServerErrors);

                    // This next step checks the error return on each item we attempted to
                    // register.  If an item is in error it's associated controls will be
                    // disabled.  If all items are in error then the Add Item button will
                    // remain active.
                    bool AnItemIsGood = false;
                    AnItemIsGood = false;
                    for (short i = 1; i <= NUMITEMS; i++)
                    {
                        if (AddItemServerErrors[i] == 0) //If the item was added successfully then allow it to be used.
                        {
                            OPCItemValueToWrite(i).Enabled = true;
                            OPCItemWriteButton(i).Enabled = true;
                            OPCItemActiveState(i).Enabled = true;
                            OPCItemSyncReadButton(i).Enabled = true;

                            AnItemIsGood = true;
                            OPCItemValue(i).Enabled = true;
                        }
                        else
                        {
                            ItemServerHandles[i] = 0; // If the handle was bad mark it as empty
                            OPCItemValueToWrite(i).Enabled = false;
                            OPCItemWriteButton(i).Enabled = false;
                            OPCItemActiveState(i).Enabled = false;
                            OPCItemSyncReadButton(i).Enabled = false;

                            OPCItemValue(i).Enabled = false;
                            OPCItemValue(i).Text = "OPC Add Item Fail";
                        }
                    }

                    // Disable the Add OPC item button if any item in the list was good
                    object Response;
                    if (AnItemIsGood)
                    {
                        OPCAddItems.Enabled = false;

                        for (short i = 1; i <= NUMITEMS; i++)
                        {
                            OPCItemName(i).Enabled = false; // Disable the Item Name cotnrols while now that they have been added to the group.
                        }

                        RemoveOPCGroup.Enabled = false; // If an item has been added don't allow the group to be removed until the item is removed
                        OPCRemoveItems.Enabled = true;
                    }
                    else
                    {
                        // The OPC Server did not accept any of the items we attempted to enter, let the user know to try again.
                        MessageBox.Show("The OPC Server has not accepted any of the item you have entered, check your item names and try again.", "OPC Add Item", MessageBoxButtons.OK);
                    }

                }
                catch (Exception ex)
                {
                    // Error handling
                    MessageBox.Show("OPC server add items failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
                }
            }
        }
    }
}
