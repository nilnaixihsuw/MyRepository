using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpLink
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

const int NUMITEMS = 10;

// Also note what the actual array size is.( NUMITEMS SPECIFIES Upper Bound of Array )
// To summarize then:
//	1) Array size = 11
//	2) We use indexes 1 thru 10
//	3) Index 0 is not used at all
const int ACTUAL_ARRAY_SIZE = NUMITEMS + 1;

// Server and group related data
// The OPCServer objects must be declared here due to the use of WithEvents
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

public enum CanonicalDataTypes
{
	CanonDtByte = 17,
	CanonDtChar = 16,
	CanonDtWord = 18,
	CanonDtShort = 2,
	CanonDtDWord = 19,
	CanonDtLong = 3,
	CanonDtFloat = 4,
	CanonDtDouble = 5,
	CanonDtBool = 11,
	CanonDtString = 8
}

// General startup initialization
private void SimpleOPCInterface_Load(System.Object sender, System.EventArgs e)
{
	// Initialize controls
	AvailableOPCServerList.Items.Add("Click on \'List OPC Servers\' to start");
	
	// Initialize arrays for iterative access to controls
	OPCItemName[1] = _OPCItemName_0;
	OPCItemName[2] = _OPCItemName_1;
	OPCItemName[3] = _OPCItemName_2;
	OPCItemName[4] = _OPCItemName_3;
	OPCItemName[5] = _OPCItemName_4;
	OPCItemName[6] = _OPCItemName_5;
	OPCItemName[7] = _OPCItemName_6;
	OPCItemName[8] = _OPCItemName_7;
	OPCItemName[9] = _OPCItemName_8;
	OPCItemName[10] = _OPCItemName_9;
	
	OPCItemValue[1] = _OPCItemValue_0;
	OPCItemValue[2] = _OPCItemValue_1;
	OPCItemValue[3] = _OPCItemValue_2;
	OPCItemValue[4] = _OPCItemValue_3;
	OPCItemValue[5] = _OPCItemValue_4;
	OPCItemValue[6] = _OPCItemValue_5;
	OPCItemValue[7] = _OPCItemValue_6;
	OPCItemValue[8] = _OPCItemValue_7;
	OPCItemValue[9] = _OPCItemValue_8;
	OPCItemValue[10] = _OPCItemValue_9;
	
	OPCItemValueToWrite[1] = _OPCItemValueToWrite_0;
	OPCItemValueToWrite[2] = _OPCItemValueToWrite_1;
	OPCItemValueToWrite[3] = _OPCItemValueToWrite_2;
	OPCItemValueToWrite[4] = _OPCItemValueToWrite_3;
	OPCItemValueToWrite[5] = _OPCItemValueToWrite_4;
	OPCItemValueToWrite[6] = _OPCItemValueToWrite_5;
	OPCItemValueToWrite[7] = _OPCItemValueToWrite_6;
	OPCItemValueToWrite[8] = _OPCItemValueToWrite_7;
	OPCItemValueToWrite[9] = _OPCItemValueToWrite_8;
	OPCItemValueToWrite[10] = _OPCItemValueToWrite_9;
	
	OPCItemWriteButton[1] = _OPCItemWriteButton_0;
	OPCItemWriteButton[2] = _OPCItemWriteButton_1;
	OPCItemWriteButton[3] = _OPCItemWriteButton_2;
	OPCItemWriteButton[4] = _OPCItemWriteButton_3;
	OPCItemWriteButton[5] = _OPCItemWriteButton_4;
	OPCItemWriteButton[6] = _OPCItemWriteButton_5;
	OPCItemWriteButton[7] = _OPCItemWriteButton_6;
	OPCItemWriteButton[8] = _OPCItemWriteButton_7;
	OPCItemWriteButton[9] = _OPCItemWriteButton_8;
	OPCItemWriteButton[10] = _OPCItemWriteButton_9;
	
	OPCItemActiveState[1] = _OPCItemActiveState_0;
	OPCItemActiveState[2] = _OPCItemActiveState_1;
	OPCItemActiveState[3] = _OPCItemActiveState_2;
	OPCItemActiveState[4] = _OPCItemActiveState_3;
	OPCItemActiveState[5] = _OPCItemActiveState_4;
	OPCItemActiveState[6] = _OPCItemActiveState_5;
	OPCItemActiveState[7] = _OPCItemActiveState_6;
	OPCItemActiveState[8] = _OPCItemActiveState_7;
	OPCItemActiveState[9] = _OPCItemActiveState_8;
	OPCItemActiveState[10] = _OPCItemActiveState_9;
	
	OPCItemSyncReadButton[1] = _OPCItemSyncReadButton_0;
	OPCItemSyncReadButton[2] = _OPCItemSyncReadButton_1;
	OPCItemSyncReadButton[3] = _OPCItemSyncReadButton_2;
	OPCItemSyncReadButton[4] = _OPCItemSyncReadButton_3;
	OPCItemSyncReadButton[5] = _OPCItemSyncReadButton_4;
	OPCItemSyncReadButton[6] = _OPCItemSyncReadButton_5;
	OPCItemSyncReadButton[7] = _OPCItemSyncReadButton_6;
	OPCItemSyncReadButton[8] = _OPCItemSyncReadButton_7;
	OPCItemSyncReadButton[9] = _OPCItemSyncReadButton_8;
	OPCItemSyncReadButton[10] = _OPCItemSyncReadButton_9;
	
	OPCItemQuality[1] = _OPCItemQuality_0;
	OPCItemQuality[2] = _OPCItemQuality_1;
	OPCItemQuality[3] = _OPCItemQuality_2;
	OPCItemQuality[4] = _OPCItemQuality_3;
	OPCItemQuality[5] = _OPCItemQuality_4;
	OPCItemQuality[6] = _OPCItemQuality_5;
	OPCItemQuality[7] = _OPCItemQuality_6;
	OPCItemQuality[8] = _OPCItemQuality_7;
	OPCItemQuality[9] = _OPCItemQuality_8;
	OPCItemQuality[10] = _OPCItemQuality_9;
}

// This sub handles gathering a list of available OPC Servers and displays them
// The OPCServer Object provides a method called 'GetOPCServers' that will allow
// you to get a list of the OPC Servers that are installed on your machine.  The
// list is retured as a string array.
private void ListOPCServers_Click(System.Object sender, System.EventArgs e)
{
	try
	{
		// Create a temporary OPCServer object and use it to get the list of
		// available OPC Servers
		AnOPCServer = new OPCAutomation.OPCServer();
		
		// Clear the list control used to display them
		AvailableOPCServerList.Items.Clear();
		
		// Obtain the list of available OPC servers
		object AllOPCServers = null;
		AllOPCServers = AnOPCServer.GetOPCServers;
		
		// Load the list returned into the List box for user selection
		short i = 0;
		for (i = 0; i <= (AllOPCServers.Length - 1); i++)
		{
			AvailableOPCServerList.Items.Add(AllOPCServers[i]);
		}
		
		// Release the temporary OPCServer object now that we're done with it
		AnOPCServer = null;
		
	}
	catch (Exception ex)
	{
		// Error handling
		MessageBox.Show("List OPC servers failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
	}
}

// This sub loads the OPC Server name when selected from the list
// and places it in the OPCServerName object
private void AvailableOPCServerList_SelectedIndexChanged(System.Object sender, System.EventArgs e)
{
	// When a user selects a server from the list box its name is placed
	// in the OPCServerName
	OPCServerName.Text = AvailableOPCServerList.Text;
}

// This sub handles connecting with the selected OPC Server
// The OPCServer Object provides a method called 'Connect' that allows you
// to 'connect' with an OPC server.  The 'Connect' method can take two arguments,
// a server name and a Node name.  The Node name is optional and does not have to
// be used to connect to a local server.  When the 'Connect' method is called you
// should see the OPC Server application start if it is not aleady running.
//
//Special Note: When connect remotely to another PC running the KepserverEx make
//sure that you have properly configured DCOM on both PC's. You will find documentation
//explaining exactly how to do this on your installation CD or at the Kepware web site.
//The web site is www.kepware.com.
private void OPCServerConnect_Click(System.Object sender, System.EventArgs e)
{
	// Test to see if the User has entered or selected an OPC server name yet if not post a message
	if (OPCServerName.Text.ToString().IndexOf("Click") + 1 == 0)
	{
		try
		{
			//Create a new OPC Server object
			ConnectedOPCServer = new OPCAutomation.OPCServer();
			
			//Attempt to connect with the server
			ConnectedOPCServer.Connect(OPCServerName.Text, OPCNodeName.Text);
			
			// Throughout this example you will see a lot of code that simply enables
			// and disables the various controls on the form.  The purpose of these
			// actions is to demonstrate and insure the proper sequence of events when
			// making an OPC connection.
			// If we successfully connect to a server allow the user to disconnect
			DisconnectFromServer.Enabled = true;
			
			// Don't allow a reconnect until the user disconnects
			OPCServerConnect.Enabled = false;
			AvailableOPCServerList.Enabled = false;
			OPCServerName.Enabled = false;
			
			// Enable the group controls now that we have a server connection
			OPCGroupName.Enabled = true;
			GroupUpdateRate.Enabled = true;
			GroupDeadBand.Enabled = true;
			GroupActiveState.Enabled = true;
			AddOPCGroup.Enabled = true; // Remove group isn't enable until a group has been added
			
		}
		catch (Exception ex)
		{
			// Error handling
			DisconnectFromServer.Enabled = false;
			ConnectedOPCServer = null;
			MessageBox.Show("OPC server connect failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
		
	}
	else
	{
		// A server name has not been selected yet post an error to the user
		MessageBox.Show("You must first select an OPC Server, Click on the \'List OPC Servers\' button and select a server", "OPC Server Connect", MessageBoxButtons.OK);
	}
}

// This sub handles disconnecting from the OPC Server.  The OPCServer Object
// provides the method 'Disconnect'.  Calling this on an active OPCSerer
// object will release the OPC Server interface with your application.  When
// this occurs you should see the OPC server application shut down if it started
// automatically on the OPC connect. This step should not occur until the group
// and items have been removed
private void DisconnectFromServer_Click(System.Object sender, System.EventArgs e)
{
	// Test to see if the OPC Server connection is currently available
	if (!(ConnectedOPCServer == null))
	{
		try
		{
			//Disconnect from the server, This should only occur after the items and group
			// have been removed
			ConnectedOPCServer.Disconnect();
		}
		catch (Exception ex)
		{
			// Error handling
			MessageBox.Show("OPC server disconnect failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
		finally
		{
			// Release the old instance of the OPC Server object and allow the resources
			// to be freed
			ConnectedOPCServer = null;
			
			// Allow a reconnect once the disconnect completes
			OPCServerConnect.Enabled = true;
			AvailableOPCServerList.Enabled = true;
			OPCServerName.Enabled = true;
			
			// Don't alllow the Disconnect to be issued now that the connection is closed
			DisconnectFromServer.Enabled = false;
			
			// Disable the group controls now that we no longer have a server connection
			OPCGroupName.Enabled = false;
			GroupUpdateRate.Enabled = false;
			GroupDeadBand.Enabled = false;
			GroupActiveState.Enabled = false;
			AddOPCGroup.Enabled = false;
		}
	}
}

// This sub handles adding the group to the OPC server and establishing the
// group interface.  When adding a group you can preset some of the group
// parameters using the properties '.DefaultGroupIsActive'
// and '.DefaultGroupDeadband'.  Set these before adding the group. Once the
// group has been successfully added you can change these same settings
// along with the group update rate on the fly using the properties on the
// resulting OPCGroup object.
private void AddOPCGroup_Click(System.Object sender, System.EventArgs e)
{
	try
	{
		// Set the desire active state for the group
		ConnectedOPCServer.OPCGroups.DefaultGroupIsActive = GroupActiveState.CheckState;
		
		//Set the desired percent deadband
		ConnectedOPCServer.OPCGroups.DefaultGroupDeadband = Val(GroupDeadBand.Text);
		
		// Add the group and set its update rate
		ConnectedGroup = ConnectedOPCServer.OPCGroups.Add(OPCGroupName.Text);
		
		// Set the update rate for the group
		ConnectedGroup.UpdateRate = Val(GroupUpdateRate.Text);
		
		// ****************************************************************
		// Mark this group to receive asynchronous updates via the DataChange event.
		// This setting is IMPORTANT. Without setting '.IsSubcribed' to True your
		// VB application will not receive DataChange notifications.  This will
		// make it appear that you have not properly connected to the server.
		ConnectedGroup.IsSubscribed = true;
		
		//*****************************************************************
		// Now that a group has been added disable the Add group Button and enable
		// the Remove group Button.  This demo application adds only a single group
		OPCGroupName.Enabled = false;
		AddOPCGroup.Enabled = false;
		RemoveOPCGroup.Enabled = true;
		
		// Enable the OPC item controls now that a group has been added
		OPCAddItems.Enabled = true;
		
		for (short i = 1; i <= NUMITEMS; i++)
		{
			OPCItemName[i].Enabled = true;
		}
		
		// Disable the Disconnect Server button since we now have a group that must be removed first
		DisconnectFromServer.Enabled = false;
		
	}
	catch (Exception ex)
	{
		// Error handling
		MessageBox.Show("OPC server add group failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
	}
}

// This sub handles removing a group from the OPC server, this must be done after
// items have been removed.  The 'Remove' method allows a group to be removed
// by name from the OPC Server.  If your application will maintains more than
// one group you will need to keep a list of the group names for use in the
// 'Remove' method.  In this demo there is only one group.  The name is maintained
// in the OPCGroupName TextBox but it can not be changed once the group is added.
private void RemoveOPCGroup_Click(System.Object sender, System.EventArgs e)
{
	// Test to see if the OPC Group object is currently available
	if (!(ConnectedGroup == null))
	{
		try
		{
			// Remove the group from the server
			ConnectedOPCServer.OPCGroups.Remove(OPCGroupName.Text);
		}
		catch (Exception ex)
		{
			// Error handling
			MessageBox.Show("OPC server remove group failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
		finally
		{
			ConnectedGroup = null;
			
			// Enable the Add group Button and disable the Remove group Button
			OPCGroupName.Enabled = true;
			AddOPCGroup.Enabled = true;
			RemoveOPCGroup.Enabled = false;
			
			// Disable the item controls now that a group has been removed.
			// Items can't be added without a group so prevent the user from editing them.
			OPCAddItems.Enabled = false;
			OPCRemoveItems.Enabled = false;
			
			for (short i = 1; i <= NUMITEMS; i++)
			{
				OPCItemName[i].Enabled = false;
			}
			
			// Enable the Disconnect Server button since we have removed the group and can disconnect from the server properly
			DisconnectFromServer.Enabled = true;
		}
	}
}

// This sub allows the group's update rate to be changed on the fly.  The
// '.UpdateRate' property allows you to control how often data from this
// group will be returned to your application in the 'DataChange' event.
// The '.UpdateRate' property can be used to control and improve the overall
// performance of you application.  In this example you can see that the update
// rate is set for maximum update speed.  In a demo that's OK.  In your real
// world application, forcing the OPC Server to gather all of the OPC items in
// a group at their fastest rate may not be ideal.  In applications where you
// have data that needs to be acquired at different rates you can create
// multiple groups each with its own update rate.  Using multiple groups would
// allow you to gather time critical data in GroupA with an update rate
// of 200 millliseconds, and gather low priority data from GroupB with an
// update rate of 7000 milliseconds.  The lowest value for the '.UpdateRate'
// is 0 which tells the OPC Server go as fast as possible.  The maximium is
// 2147483647 milliseconds which is about 596 hours.
private void GroupUpdateRate_TextChanged(System.Object sender, System.EventArgs e)
{
	// If the group has been added and exist then change its update rate
	if (!(ConnectedGroup == null))
	{
		try
		{
			ConnectedGroup.UpdateRate = Val(GroupUpdateRate.Text);
			
		}
		catch (Exception ex)
		{
			MessageBox.Show("OPC server group update rate change failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
	}
}

// This sub allows the group's deadband to be changed on the fly.  Like the
// '.IsActive' property, the '.DeadBand' property can be changed at any time.
// The Deadband property allows you to control how much change must occur in
// an OPC item in this group before the value will be reported in the 'DataChange'
// event.  The value entered for '.DeadBand' is 0 to 100 as a percentage of full
// scale for each OPC item data type within this group.  If your OPC item is a
// Short(VT_I2) then your full scale is -32768 to 32767 or 65535.  If you
// enter a Deadband value of 1% then all OPC Items in this goup would need
// to change by a value of 655 before the change would be returned in the
// 'DataChange' event.  The '.DeadBand' property is a floating point number
// allowing very small ranges of change to be filtered.
private void GroupDeadBand_TextChanged(System.Object sender, System.EventArgs e)
{
	// If the group has been added and exist then change its dead band
	if (!(ConnectedGroup == null))
	{
		try
		{
			ConnectedGroup.DeadBand = Val(GroupDeadBand.Text);
			
		}
		catch (Exception ex)
		{
			// Error handling
			MessageBox.Show("OPC server group deadband change failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
	}
}

// This sub allows the group's active state to be changed on the fly.  The
// OPCGroup object provides a number of properties that can be used to control
// a group's operation.  The '.IsActive' property allows you to turn all of the
// OPC items in the group On(active) and Off(inactive).  To see the effect that
// the group's '.InActive' property has on an OPC Server run this demo and connect
// with KEPServerEx, add the default group, add the default items.  Once you see
// changing data click on the CheckBox in the Group frame.  If you watch
// the KEPServerEx OPC Server you will see it's active tag count go from 10 when
// updating to 'No Active Items' when the group is made inactive.
// Changing the actvie state of a group can be useful in controlling how your
// application makes use of an OPC Servers communication bandwidth.  If you don't
// need any of the data in a given group simply set it inactive, this will allow an
// OPC Server to gather only the data current required by your application.
private void GroupActiveState_CheckedChanged(System.Object sender, System.EventArgs e)
{
	// If the group has been added and exist then change its active state
	if (!(ConnectedGroup == null))
	{
		try
		{
			ConnectedGroup.IsActive = GroupActiveState.CheckState;
			
		}
		catch (Exception ex)
		{
			// Error handling
			MessageBox.Show("OPC server group active state change failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
	}
}

// This sub handles adding an OPC item to a group.  The group must be established first before
// any items can be added.  Once you  have a group added to the OPC Server you
// need to add item to the group.  The OPCItems object provides the methods and
// properties need to add item to an estabished OPC group.
private void OPCAddItems_Click(System.Object sender, System.EventArgs e)
{
	// Test to see if the OPC Group object is currently available
	if (!(ConnectedGroup == null))
	{
		try
		{
			int ItemCount = NUMITEMS;
			
			// Array for potential error returns.  This example doesn't
			// check them but yours should ultimately.
			System.Array AddItemServerErrors = default(System.Array);
			
			// Load the request OPC Item names and build the ClientHandles list
			for (short i = 1; i <= NUMITEMS; i++)
			{
				// Load the name of then item to be added to this group.  You can add
				// as many items as you want to the group in a single call by building these
				// arrays as needed.
				OPCItemIDs[i] = System.Convert.ToString(OPCItemName[i].Text);
				
				// ASSume all aren't an array. If it is, this holds size and is set in
				// Data change event.
				OPCItemIsArray[i] = 0;
				
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
				OPCItemActiveState[i].CheckState = System.Windows.Forms.CheckState.Checked;
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
			bool AnItemIsGood = default(bool);
			AnItemIsGood = false;
			for (short i = 1; i <= NUMITEMS; i++)
			{
				if (AddItemServerErrors[i] == 0) //If the item was added successfully then allow it to be used.
				{
					OPCItemValueToWrite[i].Enabled = true;
					OPCItemWriteButton[i].Enabled = true;
					OPCItemActiveState[i].Enabled = true;
					OPCItemSyncReadButton[i].Enabled = true;
					
					AnItemIsGood = true;
					OPCItemValue[i].Enabled = true;
				}
				else
				{
					ItemServerHandles[i] = 0; // If the handle was bad mark it as empty
					OPCItemValueToWrite[i].Enabled = false;
					OPCItemWriteButton[i].Enabled = false;
					OPCItemActiveState[i].Enabled = false;
					OPCItemSyncReadButton[i].Enabled = false;
					
					OPCItemValue[i].Enabled = false;
					OPCItemValue[i].Text = "OPC Add Item Fail";
				}
			}
			
			// Disable the Add OPC item button if any item in the list was good
			object Response;
			if (AnItemIsGood)
			{
				OPCAddItems.Enabled = false;
				
				for (short i = 1; i <= NUMITEMS; i++)
				{
					OPCItemName[i].Enabled = false; // Disable the Item Name cotnrols while now that they have been added to the group.
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

// This sub handles removing OPC items from a group.  Like the 'AddItems' method
// of the OPCItems object, the 'Remove' method allow us to remove item from
// an OPC group.  In this example we are removing all item from the group.
// In your application you may find it necessary to remove some items from
// a group while ading others.  Normally the best practice however it to add
// all the item you wish to use to the group and then control their active
// states indivudually.  You can control the active state of individual items
// in a group as shown in the 'OPCItemActiveState_Click' subroutine of this
// module.  With that said if you intend to remove the group you
// should first remove all its items.  The 'Remove' method uses the
// ItemServerHandles we received from the 'AddItems' method to properly remove
// only the items you wish.  This is an example of how ItemServerHandles are
// used by your application and the OPC Server.  As stated above, you can
// design your application to add and remove items as needed but that's not
// necessarily the most effiecent operation for the OPC Server.
private void OPCRemoveItems_Click(System.Object sender, System.EventArgs e)
{
	if (!(ConnectedGroup == null))
	{
		if (ConnectedGroup.OPCItems.Count != 0)
		{
			try
			{
				// Provide an array to contain the ItemServerHandles of the item
				// we intend to remove
				int[] RemoveItemServerHandles = new int[NUMITEMS + 1];
				
				// Array for potential error returns.  This example doesn't
				// check them but yours should ultimately.
				System.Array RemoveItemServerErrors = default(System.Array);
				
				// Get the Servers handle for the desired items.  The server handles
				// were returned in add item subroutine.  In this case we need to get
				// only the handles for item that are valid.
				short ItemCount = (short) 0;
				for (short i = 1; i <= NUMITEMS; i++)
				{
					// In this example if the ItemServerHandle is non zero it is valid
					if (ItemServerHandles[i] != 0)
					{
						ItemCount++;
						RemoveItemServerHandles[ItemCount] = ItemServerHandles[i];
					}
				}
				
				// Invoke the Remove Item operation.  Remember this call will
				// wait until completion
				ConnectedGroup.OPCItems.Remove(ItemCount, RemoveItemServerHandles, RemoveItemServerErrors);
				
				for (short i = 1; i <= ItemCount; i++)
				{
					if (RemoveItemServerErrors[i] != 0)
					{
						MessageBox.Show("OPC server remove item failed with error: " + RemoveItemServerErrors[i], "OPC remove item", MessageBoxButtons.OK);
					}
				}
				
			}
			catch (Exception ex)
			{
				// Error handling
				MessageBox.Show("OPC server remove items failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
			}
			finally
			{
				
				// Clear the ItemServerHandles and turn off the controls for interacting
				// with the OPC items on the form.
				for (short i = 1; i <= NUMITEMS; i++)
				{
					ItemServerHandles[i] = 0; //Mark the handle as empty
					OPCItemValueToWrite[i].Enabled = false;
					OPCItemWriteButton[i].Enabled = false;
					OPCItemActiveState[i].Enabled = false;
					OPCItemSyncReadButton[i].Enabled = false;
				}
				
				// Enable the Add OPC item button and Remove Group button now that the
				// items are released
				OPCAddItems.Enabled = true;
				RemoveOPCGroup.Enabled = true;
				OPCRemoveItems.Enabled = false;
				
				// Enable the OPC Item name controls to allow a new set of items
				// to be entered
				for (short i = 1; i <= NUMITEMS; i++)
				{
					OPCItemName[i].Enabled = true;
					
					OPCItemIsArray[i] = 0;
				}
			}
		}
	}
}

private bool LoadArray(ref System.Array AnArray, short CanonDT, string wrTxt)
{
	int ii = 0;
	int loc = 0;
	int Wlen = 0;
	int start = 0;
	
	try
	{
		start = 1;
		Wlen = wrTxt.Length;
		for (ii = AnArray.GetLowerBound(0); ii <= AnArray.GetUpperBound(0); ii++)
		{
			loc = wrTxt.IndexOf(",", start - 1) + 1;
			if (ii < AnArray.GetUpperBound(0))
			{
				if (loc == 0)
				{
                    MessageBox.Show("Write Value: Incorrect Number of Items for Array Size?");
					return false;
				}
			}
			else
			{
				loc = Wlen + 1;
			}
			
			switch (CanonDT)
			{
				case  (short)CanonicalDataTypes.CanonDtByte:
                    //AnArray(ii) = (System.Array) (Convert.ToByte(wrTxt.Substring(start - 1, loc - start))); 
                    AnArray.SetValue(Convert.ToByte(wrTxt.Substring(start - 1, loc - start)),ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtChar:
                    //AnArray(ii) = (System.Array) (Convert.ToSByte(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToSByte(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtWord:
                    //AnArray(ii) = (System.Array) (Convert.ToUInt16(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToUInt16(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtShort:
                    //AnArray(ii) = (System.Array) (Convert.ToInt16(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToInt16(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtDWord:
                    //AnArray(ii) = (System.Array) (Convert.ToUInt32(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToUInt32(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtLong:
                    //AnArray(ii) = (System.Array) (Convert.ToInt32(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToInt32(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtFloat:
                    //AnArray(ii) = (System.Array) (Convert.ToSingle(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToSingle(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtDouble:
                    //AnArray(ii) = (System.Array) (Convert.ToDouble(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToDouble(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtBool:
                    //AnArray(ii) = (System.Array) (Convert.ToBoolean(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToBoolean(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case

                case (short)CanonicalDataTypes.CanonDtString:
                    //AnArray(ii) = (System.Array) (Convert.ToString(wrTxt.Substring(start - 1, loc - start)));
                    AnArray.SetValue(Convert.ToString(wrTxt.Substring(start - 1, loc - start)), ii);
					break;
					// End case
					
				default:
                    MessageBox.Show("Write Value Unknown data type");					
					return false;
			}
			
			start = loc + 1;
		}
		
		return true;
	}
	catch (Exception ex)
	{
        MessageBox.Show("Write Value generated Exception: " + ex.Message, "SimpleOPCInterface Exception");
		return false;
	}
}

// This sub handles writing a single value to the server using the
// 'SyncWrite' write method.  The 'SyncWrite' method provides a
// quick(programming wise) means to send a value to an OPC Server.  The item
// you intend to write must already be part of an OPC group you have added
// and you must have the ItemServerHandle for the item.  This is another example
// of how the ItemServerHandle is used and why it is important to properly
// store and track these handles.  The 'SyncWrite' method while quick and easy
// will wait for the OPC Server to complete the operation.  Once you invoke
// the 'SyncWrite' method it could take a moment for the OPC Server to return
// control to your application.  For this example that's OK.  If your application
// can't tolerate a pause you can use the 'AsyncWrite' and its associated
// 'AsyncWriteComplete' call back event instead.  In this sub we are only
// writing one value at a time.  The 'SyncWrite' mehtod can take a list of
// writes to be performed allow you to write entire recipes to the server
// in one shot.  If you are going to write more than one item, the
// ItemServerHandles for each item must be from the same OPC Group.
private void OPCItemWriteButton_Click(System.Object sender, System.EventArgs e)
{
	if (!(ConnectedGroup == null))
	{
		// Get control index from name
		short index = (short) (-1);
		
		if (sender.Name == "_OPCItemWriteButton_0")
		{
			index = (short) 1;
		}
		else if (sender.Name == "_OPCItemWriteButton_1")
		{
			index = (short) 2;
		}
		else if (sender.Name == "_OPCItemWriteButton_2")
		{
			index = (short) 3;
		}
		else if (sender.Name == "_OPCItemWriteButton_3")
		{
			index = (short) 4;
		}
		else if (sender.Name == "_OPCItemWriteButton_4")
		{
			index = (short) 5;
		}
		else if (sender.Name == "_OPCItemWriteButton_5")
		{
			index = (short) 6;
		}
		else if (sender.Name == "_OPCItemWriteButton_6")
		{
			index = (short) 7;
		}
		else if (sender.Name == "_OPCItemWriteButton_7")
		{
			index = (short) 8;
		}
		else if (sender.Name == "_OPCItemWriteButton_8")
		{
			index = (short) 9;
		}
		else
		{
			index = (short) 10;
		}
		
		try
		{
			// Write only 1 item
			short ItemCount = (short) 1;
			
			// Create some local scope variables to hold the value to be sent.
			// These arrays could just as easily contain all of the item we have added.
			int[] SyncItemServerHandles = new int[2];
			object[] SyncItemValues = new object[2];
			System.Array SyncItemServerErrors = default(System.Array);
			OPCAutomation.OPCItem AnOpcItem = default(OPCAutomation.OPCItem);
			
			// Get the Servers handle for the desired item.  The server handles
			// were returned in add item subroutine.
			SyncItemServerHandles[1] = ItemServerHandles[index];
			AnOpcItem = ConnectedGroup.OPCItems.GetOPCItem(ItemServerHandles[index]);
			
			// Load the value to be written using Item's Canonical Data Type to
			// convert to correct type.
			// See Kepware Application note on Canonical Data Types
			Array ItsAnArray = default(Array);
			short CanonDT = 0;
			CanonDT = System.Convert.ToInt16(AnOpcItem.CanonicalDataType());
			
			// If it is an array, figure out the base type
			if (CanonDT > Constants.vbArray)
			{
				CanonDT -= (short) Constants.vbArray;
			}
			
			switch (CanonDT)
			{
				case CanonicalDataTypes.CanonDtByte:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(byte), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToByte(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtChar:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(SByte), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToSByte(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtWord:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(UInt16), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToUInt16(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtShort:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(short), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToInt16(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtDWord:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(UInt32), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToUInt32(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtLong:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(int), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToInt32(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtFloat:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(Single), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToSingle(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtDouble:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(double), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToDouble(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtBool:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(bool), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToBoolean(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				case CanonicalDataTypes.CanonDtString:
					if (OPCItemIsArray[index] > 0)
					{
						ItsAnArray = (Array) (Array.CreateInstance(typeof(string), OPCItemIsArray[index]));
						if (!LoadArray(ref ItsAnArray, CanonDT, System.Convert.ToString(OPCItemValueToWrite[index].Text)))
						{
							return ;
						}
						SyncItemValues[1] = (object) ItsAnArray;
					}
					else
					{
						SyncItemValues[1] = Convert.ToString(OPCItemValueToWrite[index].Text);
					}
					break;
					// End case
					
				default:
					Interaction.MsgBox("OPCItemWriteButton Unknown data type", MsgBoxStyle.Exclamation, null);
					return ;
					// End case
			}
			
			// Invoke the SyncWrite operation.  Remember this call will wait until completion
			ConnectedGroup.SyncWrite(ItemCount, SyncItemServerHandles, SyncItemValues, SyncItemServerErrors);
			
			if (SyncItemServerErrors[1] != 0)
			{
MessageBox.Show("SyncItemServerError: " + System.Convert.ToString(SyncItemServerErrors[1]));
			}
		}
		catch (Exception ex)
		{
			// Error handling
			MessageBox.Show("OPC server write item failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
	}
}

// This sub handles performing a single synchronous read from a single item
// using the 'SyncRead' method.  The 'SyncRead' method like the 'SyncWrite',
// will wait for comletion from the server before returning to your application.
// There are two sources for data an OPC Server can return to your application.
// The first source is from 'Cache' the other is from 'Device'.  The 'SyncRead'
// method allows you to choose where you want to get the data.  If you choose
// 'Cache' the data has the potential to be old data which you can determine by
// looking at the time stamp on the data.  If you know that the data you are
// requesting is actively being scanned by the OPC Server you should be able to
// invoke the 'SyncRead' method using the mode selection of 'OPCCache'.  If your
// not sure if the data you desire is being scanned by the server or its out of
// date, you can use the mode selection of 'OPCDevice'.  The 'OPCDevice' mode
// commands the OPC Server to go and get this item directly from the device and
// 'DO IT NOW'.  This pretty much insures that you will receive the most recent
// value for the time your are requesting.  The downside, when reading from the
// device directly the 'SyncRead' method will wait for the device to complete
// that read operation which could include mire time, modem time, or any other
// factor that is required to gather data from the actual device.  There are some
// benefits to using a 'SyncRead'  in the 'OPCDevice' mode.  If you want to
// completely control the data acquisition cycle from your application you can
// add your groups, add your items, make the items inactive, then use the 'SyncRead'
// mehtod to forcibly make the server perform read operations when you want.
// Using this scheme the server would only talk to the the device when you invoke
// either a 'SyncRead' or 'SyncWrite' method.  In this example using the KEPServerEx
// simulator you can see this effect by connecting with KEPServerEx, adding the
// default group, adding the default items, and then clicking on the group active
// control.  With the data updates stopped you can now click on the SyncRead button
// for each item and see a single read occur.  If you look at the active tag count
// KEPServerEx you will see it momentarily increase each time you press the SyncRead
// button.
private void OPCItemSyncReadButton_Click(System.Object sender, System.EventArgs e)
{
	if (!(ConnectedGroup == null))
	{
		// Get control index from name
		short index = (short) (-1);
		
		if (sender.Name == "_OPCItemSyncReadButton_0")
		{
			index = (short) 1;
		}
		else if (sender.Name == "_OPCItemSyncReadButton_1")
		{
			index = (short) 2;
		}
		else if (sender.Name == "_OPCItemSyncReadButton_2")
		{
			index = (short) 3;
		}
		else if (sender.Name == "_OPCItemSyncReadButton_3")
		{
			index = (short) 4;
		}
		else if (sender.Name == "_OPCItemSyncReadButton_4")
		{
			index = (short) 5;
		}
		else if (sender.Name == "_OPCItemSyncReadButton_5")
		{
			index = (short) 6;
		}
		else if (sender.Name == "_OPCItemSyncReadButton_6")
		{
			index = (short) 7;
		}
		else if (sender.Name == "_OPCItemSyncReadButton_7")
		{
			index = (short) 8;
		}
		else if (sender.Name == "_OPCItemSyncReadButton_8")
		{
			index = (short) 9;
		}
		else
		{
			index = (short) 10;
		}
		
		try
		{
			// Read only 1 item
			short ItemCount = (short) 1;
			
			// Provide storage the arrays returned by the 'SyncRead' method
			int[] SyncItemServerHandles = new int[2];
			System.Array SyncItemValues = default(System.Array);
			System.Array SyncItemServerErrors = default(System.Array);
			
			// Get the Servers handle for the desired item.  The server handles were
			// returned in add item subroutine.
			SyncItemServerHandles[1] = ItemServerHandles[index];
			
			// Invoke the SyncRead operation.  Remember this call will wait until
			// completion. The source flag in this case, 'OPCDevice' , is set to
			// read from device which may take some time.
			ConnectedGroup.SyncRead(OPCAutomation.OPCDataSource.OPCDevice, ItemCount, SyncItemServerHandles, SyncItemValues, SyncItemServerErrors);
			
			// Save off the value returned after checking for error
			if (SyncItemServerErrors[1] == 0)
			{
				if (Information.IsArray(SyncItemValues[1]))
				{
					Array ItsAnArray = default(Array);
					int x = 0;
					string Suffix = "";
					
					ItsAnArray = SyncItemValues[1];
					
					OPCItemValue[index].Text = "";
					for (x = ItsAnArray.GetLowerBound(0); x <= ItsAnArray.GetUpperBound(0); x++)
					{
						if (x == ItsAnArray.GetUpperBound(0))
						{
							Suffix = "";
						}
						else
						{
							Suffix = ", ";
						}
						OPCItemValue[index].Text = 
							OPCItemValue[index].Text + ItsAnArray[x] + Suffix;
					}
				}
				else
				{
					OPCItemValue[index].Text = SyncItemValues[1];
				}
			}
			else
			{
MessageBox.Show("SyncItemServerError: " + System.Convert.ToString(SyncItemServerErrors[1]));
			}
			
		}
		catch (Exception ex)
		{
			// Error handling
			MessageBox.Show("OPC server read item failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
	}
}

// This Sub sets the active state of an individual item.  Like the other methods
// that perform operation on either a single item of list of items, the
// 'SetActive' method requires the ItemServerHandle of the item to be modified.
// Using the active state of an item allows you to control the amount of work
// the OPC Server is doing when communicating with a device.  You could add all
// the item you desire to read in an Active state but if some of those items are
// not currently in use you can improve the performance of the OPC Server by making
// those items inactive.  We suggest that you make the items not currently be used
// inactive instead of removing them from the group.  Watch the KEPServerEx
// active item count at the bottom of its windows as you change the state of
// items.
private void OPCItemActiveState_CheckedChanged(System.Object sender, System.EventArgs e)
{
	if (!(ConnectedGroup == null))
	{
		// Get control index from name
		short index = (short) (-1);
		
		if (sender.Name == "_OPCItemActiveState_0")
		{
			index = (short) 1;
		}
		else if (sender.Name == "_OPCItemActiveState_1")
		{
			index = (short) 2;
		}
		else if (sender.Name == "_OPCItemActiveState_2")
		{
			index = (short) 3;
		}
		else if (sender.Name == "_OPCItemActiveState_3")
		{
			index = (short) 4;
		}
		else if (sender.Name == "_OPCItemActiveState_4")
		{
			index = (short) 5;
		}
		else if (sender.Name == "_OPCItemActiveState_5")
		{
			index = (short) 6;
		}
		else if (sender.Name == "_OPCItemActiveState_6")
		{
			index = (short) 7;
		}
		else if (sender.Name == "_OPCItemActiveState_7")
		{
			index = (short) 8;
		}
		else if (sender.Name == "_OPCItemActiveState_8")
		{
			index = (short) 9;
		}
		else
		{
			index = (short) 10;
		}
		
		try
		{
			// Change only 1 item
			short ItemCount = (short) 1;
			
			// Dim local arrays to pass desired item for state change
			int[] ActiveItemServerHandles = new int[2];
			bool ActiveState = default(bool);
			System.Array ActiveItemErrors = default(System.Array);
			
			// Get the desired state from the control.
			ActiveState = System.Convert.ToBoolean(OPCItemActiveState[index].CheckState);
			
			// Get the Servers handle for the desired item.  The server handles
			// were returned in add item subroutine.
			ActiveItemServerHandles[1] = ItemServerHandles[index];
			
			// Invoke the SetActive operation on the OPC item collection interface
			ConnectedGroup.OPCItems.SetActive(ItemCount, ActiveItemServerHandles, ActiveState, ActiveItemErrors);
			
		}
		catch (Exception ex)
		{
			// Error handling
			MessageBox.Show("OPC server set active state failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
		}
	}
}

// This sub handles the 'DataChange' call back event which returns data that has
// been detected as changed within the OPC Server.  This call back should be
// used primarily to receive the data.  Do not make any other calls back into
// the OPC server from this call back.  The other item related functions covered
// in this example have shown how the ItemServerHandle is used to control and
// manipulate individual items in the OPC server.  The 'DataChange' event allows
// us to see how the 'ClientHandles we gave the OPC Server when adding items are
// used.  As you can see here the server returns the 'ClientHandles' as an array.
// The number of item returned in this event can change from trigger to trigger
// so don't count on always getting a 1 to 1 match with the number of items
// you have registered.  That where the 'ClientHandles' come into play.  Using
// the 'ClientHandles' returned here you can determine what data has changed and
// where in your application the data should go.  In this example the
// 'ClientHandles' were the Index number of each item we added to the group.
// Using this returned index number the 'DataChange' handler shown here knows
// what controls need to be updated with new data.  In your application you can
// make the client handles anything you like as long as they allow you to
// uniquely identify each item as it returned in this event.
private void ConnectedGroup_DataChange(int TransactionID, int NumItems, ref System.Array ClientHandles, ref System.Array ItemValues, ref System.Array Qualities, ref System.Array TimeStamps)
{
	// We don't have error handling here since this is an event called from the OPC interface
	
	try
	{
		short i = 0;
		for (i = 1; i <= NumItems; i++)
        {
			// Use the 'Clienthandles' array returned by the server to pull out the
			// index number of the control to update and load the value.
			if (Information.IsArray(ItemValues(i)))
			{
				Array ItsAnArray = default(Array);
				int x = 0;
				string Suffix = "";
				
				ItsAnArray = ItemValues(i);
				
				// Store the size of array for use by sync write
				OPCItemIsArray[ClientHandles(i)] = ItsAnArray.GetUpperBound(0) + 1;
				
				OPCItemValue[ClientHandles(i)].Text = "";
				for (x = ItsAnArray.GetLowerBound(0); x <= ItsAnArray.GetUpperBound(0); x++)
				{
					if (x == ItsAnArray.GetUpperBound(0))
					{
						Suffix = "";
					}
					else
					{
						Suffix = ", ";
					}
					OPCItemValue[ClientHandles(i)].Text = 
						OPCItemValue[ClientHandles(i)].Text + ItsAnArray[x] + Suffix;
				}
			}
			else
			{
				OPCItemValue[ClientHandles(i)].Text = ItemValues(i);
			}
			
			// Check the Qualties for each item retured here.  The actual contents of the
			// quality field can contain bit field data which can provide specific
			// error conditions.  Normally if everything is OK then the quality will
			// contain the 0xC0
			if ((Qualities(i) && OPCAutomation.OPCQuality.OPCQualityGood) == OPCAutomation.OPCQuality.OPCQualityGood)
			{
				OPCItemQuality[ClientHandles(i)].Text = "Good";
			}
			else if ((Qualities(i) && OPCAutomation.OPCQuality.OPCQualityUncertain) == OPCAutomation.OPCQuality.OPCQualityUncertain)
			{
				OPCItemQuality[ClientHandles(i)].Text = "Uncertain";
			}
			else
			{
				OPCItemQuality[ClientHandles(i)].Text = "Bad";
			}
		}
	}
	catch (Exception ex)
	{
		// Error handling
		MessageBox.Show("OPC DataChange failed with exception: " + ex.Message, "SimpleOPCInterface Exception", MessageBoxButtons.OK);
	}
}

// This sub handles exiting the example and properly disconnecting
// from the OPC server in an orderly fashion.  Like the force order
// of the controls on this form, the exit attempts to remove the Items
// from the group, then the group from the server and finally disconnect
// from the server.  This is also why each of the subroutines had the test
// to see if the Object to be removed was already set to 'Nothing'.
private void ExitExample_Click(System.Object sender, System.EventArgs e)
{
	// These calls will remove the OPC items, Group, and Disconnect in the proper order
	OPCRemoveItems_Click(OPCRemoveItems, new System.EventArgs());
	RemoveOPCGroup_Click(RemoveOPCGroup, new System.EventArgs());
	DisconnectFromServer_Click(DisconnectFromServer, new System.EventArgs());
	ProjectData.EndApp();
}
    }
}
