Option Explicit On
Imports OPCAutomation

Public Class Form1


    'Option Base 1

    '首先申明OPC对象:

    Dim WithEvents ServerObj As OPCServer  'OPC Server对象，连接OPC服务器
    Dim GroupsObj As OPCGroups             'OPC Groups对象，添加OPC组
    Dim WithEvents GroupObj As OPCGroup    'OPC Group对象
    Dim ItemsObj As OPCItems               'OPC Item集合
    Dim ServerHandles() As Long           '服务器端OPC Item的句柄
    Dim ClientHandles() As Long          '客户端OPC Item的句柄
    Dim ItemId() As String
    Dim Errors() As Long
    Dim WithEvents AnOPCServer As OPCAutomation.OPCServer

    Private Sub Command1_Click()
        '接下来，生成各个对象：
        On Error GoTo Err

        '    Get input.
        '    InputServerName = Trim(txtServerName.Text)
        '    InputServerNodeName = Trim(txtServerNodeName.Text)
        '    InputItemNumber = CInt(Trim(txtItemNumber.Text))

        '    ReDim ClientHandles(InputItemNumber)
        '    ReDim ItemId(InputItemNumber)
        ReDim ServerHandles(3)     'temp use
        ReDim ClientHandles(3)         'temp use
        ReDim ItemId(3)                'temp use

        If ServerObj Is Nothing Then ServerObj = New OPCServer

        '连接OPC服务器
        AnOPCServer = New OPCServer

  
        ' Obtain the list of available OPC servers
        Dim AllOPCServers As Object
        AllOPCServers = AnOPCServer.GetOPCServers
        'If ServerObj.ServerState = OPCDisconnected Then

        ServerObj.Connect(AllOPCServers(1)) 'temp use

        'End If

        'If ServerObj.ServerState <> OPCDisconnected() Then
        'MsgBox("Server connect successfully.")
        'Else
        'MsgBox("Server connect Error!")
        'End If


        'If GroupsObj Is Nothing Then GroupsObj = ServerObj.OPCGroups

        If GroupObj Is Nothing Then GroupObj = ServerObj.OPCGroups.Add("Channel1")
        GroupObj.IsSubscribed = True
        GroupObj.IsActive = True     '设置组为活动状态
        GroupObj.UpdateRate = 1000
        'If ItemsObj Is Nothing Then ItemsObj = GroupObj.OPCItems

        Dim i As Long

        Dim ItemCount As Integer = 3

        ItemId(1) = "Channel1.Device1.yun"

        ItemId(2) = "Channel1.Device1.yun1"

        ItemId(3) = "Channel1.Device1.yun2"

        For i = "1" To 3

            ClientHandles(i) = i

            '给客户端句柄赋值

        Next

        'ItemId(1) = "Channel1.Device1.yun"

        'ItemId(2) = "Channel1.Device1.yun1"

        'ClientHandles(1) = 1

        'ClientHandles(2) = 3
        GroupObj.OPCItems.AddItem(ItemId(1), ClientHandles(1))
        GroupObj.OPCItems.AddItem(ItemId(2), ClientHandles(2))
        GroupObj.OPCItems.AddItem(ItemId(3), ClientHandles(3))
        'GroupObj.OPCItems.AddItems(ItemCount, ItemId, ClientHandles, ServerHandles, Errors)
        'ItemsObj.AddItem(ItemId(2), ClientHandles(2))
        Dim ReadValue As System.Array
        ''Dim ItemCount As Short = 1
        MsgBox(GroupObj.OPCItems.Count)
        MsgBox(GroupObj.OPCItems.Item(1).ClientHandle)
        MsgBox(GroupObj.OPCItems.Item(1).Value)
        MsgBox(GroupObj.OPCItems.Item(1).ItemID)
        'GroupObj.OPCItems.Item(1).Value = True
        GroupObj.OPCItems.Item(2).Write(1)
        MsgBox(GroupObj.OPCItems.Item(2).Value)
        'Dim OneRead As String
        'GroupObj.SyncRead(2, ItemCount, ServerHandles, ReadValue, Errors)
        'ConnectedGroup.SyncRead(OPCAutomation.OPCDataSource.OPCDevice, ItemCount, SyncItemServerHandles, SyncItemValues, SyncItemServerErrors)
        'ItemsObj.Item(1).Write(1)
        'Call ItemsObj.AddItems(2, ItemId, ClientHandles, ServerHandles, Errors)
        'ServerObj.Disconnect()
        'ServerObj = Nothing
        'AnOPCServer = Nothing
        Exit Sub
Err:
        MsgBox(Err.Description)
    End Sub
    Private Function OPCDisconnected() As Object
        Throw New NotImplementedException
    End Function
    '读：
    Private Sub Command2_Click()
        '2.3.2异步数据读取

        ' OPC Item的服务器句柄，添加OPC Item时由服务器分配
        Try

     
        Dim TempServerHandles(1) As Long

        '事务标志符，由客户端产生，它包含的信息提供给OnReadComplete事件

        Dim TransactionID As String

        '取消标志符，服务器端产生，用于操作需要被取消的时候

        Dim CancelID As String

        '包含读取每个OPC Item时返回的信息

        Dim ErrorNr() As Long

            'TempServerHandles(1) = ServerHandles(1) '对应第一个OPC Item

            GroupObj.AsyncRead(2, TempServerHandles, ErrorNr, TransactionID, CancelID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Command1_Click()
        'Command2_Click()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MsgBox(GroupObj.OPCItems.Count)
        MsgBox(GroupObj.OPCItems.Item(1).ClientHandle)
        MsgBox(GroupObj.OPCItems.Item(1).Value)
        MsgBox(GroupObj.OPCItems.Item(1).ItemID)
    End Sub
End Class
'Dim WithEvents MyOPCServer As OPCServer '定义服务器对象变量MyOPCServer

'Dim WithEvents MyOPCGroup As OPCGroup '定义OPC组对象变量MyOPCGroup

'Set MyOPCServer = New OPCServer

'MyOPCServer.Connect " OPC.SimaticNET" ‘连接SIMATICNET 的OPC服务器

'(2) 添加OPC组对象

'Set MyOPCGroup="MyOPCServer".OPCGroups.Add("Group1")

''添加OPC组对象

'MyOPCGroup.IsSubscribed= True

''设置该组数据为后台刷新

'MyOPCGroup.IsActive = True

''设置该组为激活状态

'MyOPCGroup.UpdateRate=1000

''设置数据刷新时间为1000

'(3) 添加数据项

'Dim abItemIDs() As String

''项标识符

'Dim abClientHandles() As Long

''客户端句柄

'Dim abServerHandles() As Long

''服务器端句柄

'Dim abErrors() As Long

'Dim i As Long

'ItemCount=3

'abItemIDs(1) = " S7:[S7 connection_1]IB1"

'abItemIDs(2) = " S7:[S7 connection_1]MB1"

'abItemIDs(3) = " S7:[S7 connection_1]QB1"

'for i="1" t0 3

'abClientHandles(i) = i

''给客户端句柄赋值

'Next

'MyOPCGroup.OPCItems.AddItems ItemCount, abItemIDs, abClientHandles, abServerHandles, abErrors

''添加数据项操作

'(4) 同步数据读写

'OPC数据存取有同步方式和异步方式两种。异步读写数据复杂，需要与事件结合使用，与同步相比速度慢但准确性高。同步读写数据简单，直接使用OPCItem的方法即可。

'Dim One As OPCItem

'Dim Index As Long

''Index为标签顺序号

'Dim OneRead As String

'Dim Xie As String

'Set One = MyOPCGroup.OPCItems(Index)

'One.Read OPCCache

'OneRead = One.Value

''读数据

'MyOPCGroup.SyncWrite ItemCount, ServerHandles, valuess, Errors

''同步写数据

'MyOPCGroup.SyncRead OPCCache, ItemCount, ServerHandles, ReadValue, Errors

''同步读数据

'One.Write (Xie)

''写数据

'若只读取数据，可以使用DataChange事件，当控制器中所要访问的数据一旦发生改变时将会触发该事件，并将该数据自动读到TxtValue文本框。

'Private Sub MyOPCGroup_DataChange(ByVal TransactionID As Long, ByVal NumItems As Long, ByVal ClientHandles() As Long, ByVal ItemValues() As Object, ByVal Qualities() As Long, ByVal TimeStamps() As Date)

'    '自动刷新数据

'    Dim i As Long

'    For i = 1 To NumItems

'        txtValue(ClientHandles(i)) = ItemValues(i)

'        '获取项的值

'        txtTime(ClientHandles(i)) = TimeStamps(i)

'        '获取项的时间戳

'        txtQuantity(ClientHandles(i)) = GetQualityString(Qualities(i))

'        '获取项的品质

'    Next i

'End Sub

'(5) 断开OPC服务器

'MyOPCServer.OPCGroups.RemoveAll

''移除所有OPC Group，空出资源

'Set MyOPCGroup = Nothing

'MyOPCServer.Disconnect

''断开连接