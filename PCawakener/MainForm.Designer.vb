'
' Сделано в SharpDevelop.
' Пользователь: skorik
' Дата: 22.01.2014
' Время: 18:17
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Imports System
Imports System.Data.SQLite
Imports System.Data
Imports FileIO
Imports System.IO
Imports System.Reflection



Partial Class MainForm
	Inherits System.Windows.Forms.Form
	Structure RWOL_SERVER
        Dim HOST As String
        Dim USER As String
        Dim PASSWORD As String
        Dim WOL_PROG As String        
    End Structure
	Const MyProgTitle As String = "PCawakener"
	Dim RWOL_SERVERs() As RWOL_SERVER' = "deploy2"	
	Public Dim MAC_DB As String'  = "\\\\deploy2\distrib\scripts\DB\MAC.sq3"	
	Dim LOG_WOL As String
	Dim Log As Object
	Dim WakeUpTimeout As Integer = 180
	Dim WakeUpTimerDownCount As Integer 
	Dim WakeUpTestInterval As Integer = 15
	Dim WakeUpTestIntervalDownCount As Integer
	Dim PConline=False
	Public Dim PC_name As String
	Public Dim PC_MAC as String
	
	Dim ListBoxTimerStringIndex 'Индекс строки в listBox_LOG  для лога для индикатора програесса
	
	Dim StatusProg As Integer=0 '0 - ожидание, 1 - запущено пробуждение, 2 - открыта форма редактрования ПК
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.button_Cancel = New System.Windows.Forms.Button()
		Me.button_OK = New System.Windows.Forms.Button()
		Me.label1 = New System.Windows.Forms.Label()
		Me.comboBox_PC = New System.Windows.Forms.ComboBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.textBox_MAC = New System.Windows.Forms.MaskedTextBox()
		Me.checkBox_ShowLog = New System.Windows.Forms.CheckBox()
		Me.listBox_LOG = New System.Windows.Forms.ListBox()
		Me.checkBox_ConnectRDP = New System.Windows.Forms.CheckBox()
		Me.WakeUpTimer = New System.Windows.Forms.Timer(Me.components)
		Me.button_Add = New System.Windows.Forms.Button()
		Me.pictureBox1 = New System.Windows.Forms.PictureBox()
		Me.ToolTip_button_Add = New System.Windows.Forms.ToolTip(Me.components)
		Me.button_Delete = New System.Windows.Forms.Button()
		Me.button_edit = New System.Windows.Forms.Button()
		Me.label3 = New System.Windows.Forms.Label()
		Me.TextBox_IPadress = New System.Windows.Forms.MaskedTextBox()
		Me.TextBox_Hostname = New System.Windows.Forms.MaskedTextBox()
		Me.label4 = New System.Windows.Forms.Label()
		Me.pictureBox_TestConnect = New System.Windows.Forms.PictureBox()
		Me.timer_TestConnection = New System.Windows.Forms.Timer(Me.components)
		CType(Me.pictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.pictureBox_TestConnect,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'button_Cancel
		'
		Me.button_Cancel.BackColor = System.Drawing.Color.Transparent
		Me.button_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Black
		Me.button_Cancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black
		Me.button_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.button_Cancel.Location = New System.Drawing.Point(326, 215)
		Me.button_Cancel.Name = "button_Cancel"
		Me.button_Cancel.Size = New System.Drawing.Size(173, 48)
		Me.button_Cancel.TabIndex = 0
		Me.button_Cancel.Text = "Выход"
		Me.button_Cancel.UseVisualStyleBackColor = false
		AddHandler Me.button_Cancel.Click, AddressOf Me.Button_CancelClick
		'
		'button_OK
		'
		Me.button_OK.BackColor = System.Drawing.Color.Transparent
		Me.button_OK.Enabled = false
		Me.button_OK.FlatAppearance.BorderColor = System.Drawing.Color.Black
		Me.button_OK.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black
		Me.button_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.button_OK.Location = New System.Drawing.Point(12, 215)
		Me.button_OK.Name = "button_OK"
		Me.button_OK.Size = New System.Drawing.Size(173, 48)
		Me.button_OK.TabIndex = 1
		Me.button_OK.Text = "Разбудить"
		Me.button_OK.UseVisualStyleBackColor = false
		AddHandler Me.button_OK.Click, AddressOf Me.Button_OKClick
		'
		'label1
		'
		Me.label1.AutoSize = true
		Me.label1.Location = New System.Drawing.Point(35, 14)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(177, 17)
		Me.label1.TabIndex = 2
		Me.label1.Text = "Укажите имя компьютера"
		'
		'comboBox_PC
		'
		Me.comboBox_PC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
		Me.comboBox_PC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
		Me.comboBox_PC.FormattingEnabled = true
		Me.comboBox_PC.Location = New System.Drawing.Point(12, 34)
		Me.comboBox_PC.Name = "comboBox_PC"
		Me.comboBox_PC.Size = New System.Drawing.Size(406, 24)
		Me.comboBox_PC.TabIndex = 3
		AddHandler Me.comboBox_PC.SelectedValueChanged, AddressOf Me.ComboBox_PCSelectedValueChanged
		AddHandler Me.comboBox_PC.KeyPress, AddressOf Me.ComboBox_PCKeyPress
		'
		'label2
		'
		Me.label2.AutoSize = true
		Me.label2.Location = New System.Drawing.Point(12, 94)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(122, 17)
		Me.label2.TabIndex = 4
		Me.label2.Text = "или введите MAC"
		'
		'textBox_MAC
		'
		Me.textBox_MAC.Location = New System.Drawing.Point(12, 114)
		Me.textBox_MAC.Mask = "AA:AA:AA:AA:AA:AA"
		Me.textBox_MAC.Name = "textBox_MAC"
		Me.textBox_MAC.Size = New System.Drawing.Size(122, 23)
		Me.textBox_MAC.TabIndex = 5
		Me.textBox_MAC.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
		AddHandler Me.textBox_MAC.TextChanged, AddressOf Me.TextBox_MACTextChanged
		AddHandler Me.textBox_MAC.KeyPress, AddressOf Me.TextBox_MACKeyPress
		'
		'checkBox_ShowLog
		'
		Me.checkBox_ShowLog.AutoSize = true
		Me.checkBox_ShowLog.Location = New System.Drawing.Point(35, 156)
		Me.checkBox_ShowLog.Name = "checkBox_ShowLog"
		Me.checkBox_ShowLog.Size = New System.Drawing.Size(194, 21)
		Me.checkBox_ShowLog.TabIndex = 6
		Me.checkBox_ShowLog.Text = "Показать журнал работы"
		Me.checkBox_ShowLog.UseVisualStyleBackColor = true
		AddHandler Me.checkBox_ShowLog.CheckedChanged, AddressOf Me.CheckBox_ShowLogCheckedChanged
		'
		'listBox_LOG
		'
		Me.listBox_LOG.FormattingEnabled = true
		Me.listBox_LOG.ItemHeight = 16
		Me.listBox_LOG.Location = New System.Drawing.Point(277, 156)
		Me.listBox_LOG.Name = "listBox_LOG"
		Me.listBox_LOG.Size = New System.Drawing.Size(141, 20)
		Me.listBox_LOG.TabIndex = 7
		Me.listBox_LOG.Visible = false
		'
		'checkBox_ConnectRDP
		'
		Me.checkBox_ConnectRDP.AutoSize = true
		Me.checkBox_ConnectRDP.Checked = true
		Me.checkBox_ConnectRDP.CheckState = System.Windows.Forms.CheckState.Checked
		Me.checkBox_ConnectRDP.Location = New System.Drawing.Point(35, 183)
		Me.checkBox_ConnectRDP.Name = "checkBox_ConnectRDP"
		Me.checkBox_ConnectRDP.Size = New System.Drawing.Size(326, 21)
		Me.checkBox_ConnectRDP.TabIndex = 8
		Me.checkBox_ConnectRDP.Text = "Подключиться к удаленному рабочему столу"
		Me.checkBox_ConnectRDP.UseVisualStyleBackColor = true
		AddHandler Me.checkBox_ConnectRDP.CheckedChanged, AddressOf Me.checkBox_ConnectRDP_CheckedChanged
		'
		'WakeUpTimer
		'
		Me.WakeUpTimer.Interval = 1000
		AddHandler Me.WakeUpTimer.Tick, AddressOf Me.WakeUpTimerTick
		'
		'button_Add
		'
		Me.button_Add.AccessibleDescription = ""
		Me.button_Add.BackgroundImage = CType(resources.GetObject("button_Add.BackgroundImage"),System.Drawing.Image)
		Me.button_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.button_Add.Location = New System.Drawing.Point(424, 29)
		Me.button_Add.Name = "button_Add"
		Me.button_Add.Size = New System.Drawing.Size(32, 32)
		Me.button_Add.TabIndex = 9
		Me.ToolTip_button_Add.SetToolTip(Me.button_Add, "Добавить ПК в базу данных")
		Me.button_Add.UseVisualStyleBackColor = false
		'
		'pictureBox1
		'
		Me.pictureBox1.Location = New System.Drawing.Point(0, 0)
		Me.pictureBox1.Name = "pictureBox1"
		Me.pictureBox1.Size = New System.Drawing.Size(100, 50)
		Me.pictureBox1.TabIndex = 0
		Me.pictureBox1.TabStop = false
		'
		'button_Delete
		'
		Me.button_Delete.AccessibleDescription = ""
		Me.button_Delete.BackgroundImage = CType(resources.GetObject("button_Delete.BackgroundImage"),System.Drawing.Image)
		Me.button_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.button_Delete.Location = New System.Drawing.Point(455, 29)
		Me.button_Delete.Name = "button_Delete"
		Me.button_Delete.Size = New System.Drawing.Size(32, 32)
		Me.button_Delete.TabIndex = 10
		Me.ToolTip_button_Add.SetToolTip(Me.button_Delete, "Удалить  ПК из базы данных")
		Me.button_Delete.UseVisualStyleBackColor = false
		'
		'button_edit
		'
		Me.button_edit.AccessibleDescription = ""
		Me.button_edit.BackgroundImage = CType(resources.GetObject("button_edit.BackgroundImage"),System.Drawing.Image)
		Me.button_edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.button_edit.Location = New System.Drawing.Point(486, 29)
		Me.button_edit.Name = "button_edit"
		Me.button_edit.Size = New System.Drawing.Size(32, 32)
		Me.button_edit.TabIndex = 11
		Me.ToolTip_button_Add.SetToolTip(Me.button_edit, "Изменить  ПК в базе данных")
		Me.button_edit.UseVisualStyleBackColor = false
		AddHandler Me.button_edit.Click, AddressOf Me.Button_editClick
		'
		'label3
		'
		Me.label3.AutoSize = true
		Me.label3.Enabled = false
		Me.label3.Location = New System.Drawing.Point(191, 94)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(63, 17)
		Me.label3.TabIndex = 12
		Me.label3.Text = "IP адрес"
		Me.label3.Visible = false
		'
		'TextBox_IPadress
		'
		Me.TextBox_IPadress.Enabled = false
		Me.TextBox_IPadress.Location = New System.Drawing.Point(191, 114)
		Me.TextBox_IPadress.Mask = "AAA.AAA.AAA.AAA"
		Me.TextBox_IPadress.Name = "TextBox_IPadress"
		Me.TextBox_IPadress.Size = New System.Drawing.Size(150, 23)
		Me.TextBox_IPadress.TabIndex = 13
		Me.TextBox_IPadress.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
		Me.TextBox_IPadress.Visible = false
		'
		'TextBox_Hostname
		'
		Me.TextBox_Hostname.Enabled = false
		Me.TextBox_Hostname.Location = New System.Drawing.Point(347, 114)
		Me.TextBox_Hostname.Name = "TextBox_Hostname"
		Me.TextBox_Hostname.Size = New System.Drawing.Size(152, 23)
		Me.TextBox_Hostname.TabIndex = 15
		Me.TextBox_Hostname.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
		Me.TextBox_Hostname.Visible = false
		'
		'label4
		'
		Me.label4.AutoSize = true
		Me.label4.Enabled = false
		Me.label4.Location = New System.Drawing.Point(347, 94)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(72, 17)
		Me.label4.TabIndex = 14
		Me.label4.Text = "Hostname"
		Me.label4.Visible = false
		AddHandler Me.label4.Click, AddressOf Me.Label4Click
		'
		'pictureBox_TestConnect
		'
		Me.pictureBox_TestConnect.Image = CType(resources.GetObject("pictureBox_TestConnect.Image"),System.Drawing.Image)
		Me.pictureBox_TestConnect.InitialImage = CType(resources.GetObject("pictureBox_TestConnect.InitialImage"),System.Drawing.Image)
		Me.pictureBox_TestConnect.Location = New System.Drawing.Point(140, 105)
		Me.pictureBox_TestConnect.Name = "pictureBox_TestConnect"
		Me.pictureBox_TestConnect.Size = New System.Drawing.Size(32, 32)
		Me.pictureBox_TestConnect.TabIndex = 16
		Me.pictureBox_TestConnect.TabStop = false
		Me.pictureBox_TestConnect.Visible = false
		'
		'timer_TestConnection
		'
		Me.timer_TestConnection.Interval = 1000
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8!, 16!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.ClientSize = New System.Drawing.Size(521, 280)
		Me.Controls.Add(Me.pictureBox_TestConnect)
		Me.Controls.Add(Me.TextBox_Hostname)
		Me.Controls.Add(Me.label4)
		Me.Controls.Add(Me.TextBox_IPadress)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.button_edit)
		Me.Controls.Add(Me.button_Delete)
		Me.Controls.Add(Me.button_Add)
		Me.Controls.Add(Me.checkBox_ConnectRDP)
		Me.Controls.Add(Me.listBox_LOG)
		Me.Controls.Add(Me.checkBox_ShowLog)
		Me.Controls.Add(Me.textBox_MAC)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.comboBox_PC)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.button_OK)
		Me.Controls.Add(Me.button_Cancel)
		Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204,Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Удаленное включение компьютера"
		AddHandler Load, AddressOf Me.MainFormLoad
		CType(Me.pictureBox1,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.pictureBox_TestConnect,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private timer_TestConnection As System.Windows.Forms.Timer
	Private pictureBox_TestConnect As System.Windows.Forms.PictureBox
	Private label4 As System.Windows.Forms.Label
	Private TextBox_Hostname As System.Windows.Forms.MaskedTextBox
	Private TextBox_IPadress As System.Windows.Forms.MaskedTextBox
	Private label3 As System.Windows.Forms.Label
	Private button_edit As System.Windows.Forms.Button
	Private button_Delete As System.Windows.Forms.Button
	Private ToolTip_button_Add As System.Windows.Forms.ToolTip
	Private pictureBox1 As System.Windows.Forms.PictureBox
	Private button_Add As System.Windows.Forms.Button
	Private WakeUpTimer As System.Windows.Forms.Timer
	Private checkBox_ConnectRDP As System.Windows.Forms.CheckBox
	Private listBox_LOG As System.Windows.Forms.ListBox
	Private checkBox_ShowLog As System.Windows.Forms.CheckBox
	Private textBox_MAC As System.Windows.Forms.MaskedTextBox
	Private label2 As System.Windows.Forms.Label
	Protected Friend comboBox_PC As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
	Private button_OK As System.Windows.Forms.Button
	Private button_Cancel As System.Windows.Forms.Button
	
	Sub Button_CancelClick(sender As Object, e As EventArgs)
		me.Close		
	End Sub
	
	Sub MainFormLoad(sender As Object, e As EventArgs)	
		
		Log=listBox_LOG
		listBox_LOG.Items.Add("Загрузка...")
		'listBox_LOG.Items.Add(Application.StartupPath)		
		'listBox_LOG.Items.Add(Environment.CurrentDirectory)		
		'Environment.CurrentDirectory=Application.StartupPath
		'listBox_LOG.Items.Add(Environment.CurrentDirectory)		
		listBox_LOG.Left=button_OK.Location.X
		listBox_LOG.top=Me.Height
		listBox_LOG.Width=button_Cancel.Location.X+button_Cancel.Width-button_OK.Location.X						
		listBox_LOG.Height=256
		
		listBox_LOG.Items.Add("Чтение конфигурации...")	
		Call	GetConfigFile
		listBox_LOG.Items.Add("Чтение базы данных...")	
		If MAC_DB.StartsWith("\\") Then
			Dim MAC_DB_HOST As String() =MAC_DB.Split("\",2,StringSplitOptions.RemoveEmptyEntries)
			listBox_LOG.Items.Add("Проверка соединения с "+MAC_DB_HOST(0))	
			If Not My.Computer.Network.Ping(MAC_DB_HOST(0)) Then 
				listBox_LOG.Items.Add("ERR. Нет доступа к ПК на котором расположена БД")	
				checkBox_ShowLog.Checked=True
		        Exit Sub
		    End if	
		End If
		Dim myConnection As SQLiteConnection = New SQLiteConnection()
		Try
			myConnection.ConnectionString = "Data Source="+MAC_DB+";Version=3;"
			myConnection.Open()		
		Catch ex As Exception
			MessageBox.Show(ex.Message,"Открытие базы данных мак адресов")
			listBox_LOG.Items.Add("ERR. Не удалось открыть БД MAC адресов")	
			checkBox_ShowLog.Checked=True
		End Try	
		Dim ds As DataSet = New DataSet
	    Dim da = new SQLiteDataAdapter("select pc,mac from mac order by pc", myConnection) 
	    da.Fill(ds,"pc") 
	    comboBox_PC.DataSource=ds.Tables("pc").DefaultView
	    comboBox_PC.DisplayMember = "pc"
	    comboBox_PC.ValueMember ="mac"
	    Dim SQLcommand As SQLiteCommand
	    SQLcommand = myConnection.CreateCommand
	    SQLcommand.CommandText = "SELECT pc FROM login where login='"+Strings.UCase(Environment.UserName)+"'"
	    Try
	    	
	    	Dim oResult As Object
	    	oResult = SQLcommand.ExecuteScalar()
        	If oResult IsNot Nothing Then
           		comboBox_PC.SelectedIndex =comboBox_PC.FindString(oResult.ToString)
        	End If
	    Catch	
	    End Try
		myConnection.Close()	
		
	End Sub
	
	Sub ComboBox_PCSelectedValueChanged(sender As Object, e As EventArgs)
		PC_name=comboBox_PC.Text		
		textBox_MAC.Text=comboBox_PC.SelectedValue.ToString		
		PC_MAC=textBox_MAC.Text
	End Sub
	
	Sub TextBox_MACTextChanged(sender As Object, e As EventArgs)
	button_OK.Enabled=Not  String.IsNullOrEmpty(TextBox_MAC.Text)	
	End Sub
	'*******************************************************
	Sub Button_OKClick(sender As Object, e As EventArgs)
	'********************************************************	
	StatusProg=1
	checkBox_ShowLog.Checked=True
	
	'Запись в лог
	Try
		listBox_LOG.Items.Add("Запись в журнал "+LOG_WOL+"\"+MyProgTitle+".log"+" действий пользователя")
		Dim LOG_File = New StreamWriter(LOG_WOL+"\"+MyProgTitle+".log",True)
		
		LOG_File.WriteLine(Now+" user "+Environment.UserName+" from "+Environment.MachineName+" wakeup "+comboBox_PC.Text+" MAC="+textBox_MAC.Text)
		LOG_File.Close
	Catch
		MessageBox.Show("Не удалось записать в журнал ваше действие по пробуждению ПК. Операция пробуждения ПК не будет выполнена",MyProgTitle,MessageBoxButtons.OK,MessageBoxIcon.Error)
		Exit Sub
	End Try	
	'Запомнить выбор пользователя
	listBox_LOG.Items.Add("Запись предпочтений пользователя")
	Dim myConnection As SQLiteConnection = New SQLiteConnection()
	Try
		myConnection.ConnectionString = "Data Source="+MAC_DB+";"
		myConnection.Open()		
	Catch ex As Exception		
		listBox_LOG.Items.Add("ERR. Не удалось открыть БД MAC адресов")	
		checkBox_ShowLog.Checked=True
	End Try	
	Try		
		Dim SQLcomand As New SQLiteCommand
		SQLcomand=myConnection.CreateCommand
		SQLcomand.CommandText="INSERT OR REPLACE INTO LOGIN (pc,login) VALUES('"+comboBox_PC.Text+"','"+Strings.ucase(Environment.UserName)+"')"
		SQLcomand.ExecuteNonQuery()
		
	Catch er As Exception	
		listBox_LOG.Items.Add("Не удалось записать предпочтения пользователя.")
		listBox_LOG.Items.Add(er.Message)
	End try
	myConnection.Close
	
	'Проверить что ПК не пингуется		
		My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Beep)
		If TestConnection() Then
			listBox_LOG.Items.Add("ПК "+comboBox_PC.Text+" уже включен")
			listBox_LOG.SelectedIndex=listBox_LOG.Items.Count-1
			Exit Sub
		End If
		'Начать пробуждение
		listBox_LOG.Items.Add("Пробуждение ПК "+comboBox_PC.Text+" "+textBox_MAC.Text)		
		Dim myWOL As New WOL
		listBox_LOG.Items.Add("Посылка сигнала пробуждения с локального ПК")
		myWOL.WakeUp(textBox_MAC.Text)	
		'->RWOL
		Dim i=0
		For i=0 To RWOL_SERVERs.Length-1
			If My.Computer.Network.Ping(RWOL_SERVERs(i).HOST) Then
				listBox_LOG.Items.Add("Посылка сигнала пробуждения с ПК "+RWOL_SERVERs(i).HOST)
				listBox_LOG.SelectedIndex =	listBox_LOG.Items.Count-1				
				RunAsRemoteProcess (RWOL_SERVERs(i).HOST, _
					RWOL_SERVERs(i).WOL_PROG+" "+textBox_MAC.Text, _
					RWOL_SERVERs(i).HOST+"\"+RWOL_SERVERs(i).USER, _
					RWOL_SERVERs(i).PASSWORD)
					If _RunRemoteProcess_errorflag <> 0	
						listBox_LOG.Items.Add("ERR."+_RunRemoteProcess_errorstr )					
					End If	
				
				

			Else
				listBox_LOG.Items.Add("ERR. ПК "+RWOL_SERVERs(i).HOST+" не пингуется")
			End If		
			
		Next
		listBox_LOG.Items.Add("Манипуляция для пробуждения ПК "+comboBox_PC.Text+" завершены")				
	    '-> Таймер ожидания включения ПК и пинги.	    
	    WakeUpTimerDownCount=WakeUpTimeout*1000
	    WakeUpTestIntervalDownCount=WakeUpTestInterval*1000
	    ListBoxTimerStringIndex=-1			    
	    WakeUpTimer.Start
		'->Если Пинга нет - Варнинг и выход.
		'-> Запрос: Подключиться к удаленному ПК
		listBox_LOG.Items.Add("Манипуляция для пробуждения ПК "+comboBox_PC.Text+" завершены")		
	End Sub
	'******************************************************
	Sub RunMSTSC( byRef PC As String)
	'******************************************************
	If Not Log Is Nothing Then
		Log.Items.Add("MSTSC "+PC)
	End If
	'-> Уточнить расположение mstsc
	WindowsRun("c:\windows\system32\mstsc.exe","/v:"+PC)
	End Sub
	'******************************************************
	Sub WindowsRun (ByRef Prog As String, Optional ByRef Args as String ="")
		'******************************************************	
	If Not Log Is Nothing Then
		Log.Items.Add("Запуск программы "+Prog)
	End If	
	
	Dim p As New ProcessStartInfo
		p.FileName = Prog	
		p.Arguments = Args
		p.WindowStyle = ProcessWindowStyle.Normal
		Dim myProcess As Process = Nothing
		Try
			myProcess =Process.Start(p)	
		Catch e As Exception
			If Not Log Is Nothing Then
				Log.Items.Add(e.Message)
			End If	
		End Try
	End Sub
	'******************************************************
	Sub GetConfigFile
	'******************************************************
	Dim fso As Object		
	fso = CreateObject("Scripting.FileSystemObject")	
	Const myConfigFile As String = "PCawakener.ini"
	Dim ConfigFile As String 
	ConfigFile=Directory.GetCurrentDirectory()+"\"+myConfigFile	
	
	If  Not fso.FileExists(ConfigFile) Then
		MessageBox.Show("Файл с параметрами программы не найден.",MyProgTitle,MessageBoxButtons.OK,MessageBoxIcon.Stop)
		listBox_LOG.Items.Add("Конфигурация должна быть в файле "+ConfigFile)
		Exit Sub
	End If
	Dim inifile As New Inifile(ConfigFile)
	MAC_DB=inifile.LoadString("GENERAL", "MAC_DB", MAC_DB)
	LOG_WOL=inifile.LoadString("GENERAL", "LOG")
	listBox_LOG.Items.Add("База данных MAC адресов "+MAC_DB)
	Dim i=0
	Dim RWOL_SERVER_HOST As String	
	While True	
	 RWOL_SERVER_HOST=inifile.LoadString("RWOL_SERVER"+CStr(i+1), "HOST")
	 If Not String.IsNullOrEmpty(RWOL_SERVER_HOST) Then	 	
	 	ReDim Preserve RWOL_SERVERs(i)
	 	RWOL_SERVERs(i).HOST=RWOL_SERVER_HOST
	 	RWOL_SERVERs(i).USER=inifile.LoadString("RWOL_SERVER"+CStr(i+1), "USER")
	 	RWOL_SERVERs(i).PASSWORD=inifile.LoadString("RWOL_SERVER"+CStr(i+1), "PASSWORD")
	 	RWOL_SERVERs(i).WOL_PROG=inifile.LoadString("RWOL_SERVER"+CStr(i+1), "WOL_PROG")
	 	
	 	listBox_LOG.Items.Add("Сервер"+CStr(i+1)+" удаленного посыла MagicPacket "+RWOL_SERVER_HOST)
	 Else	
	 	Exit While
	 End If 
	 i=i+1
	 End While
	 
	End Sub
	
	Sub CheckBox_ShowLogCheckedChanged(sender As Object, e As EventArgs)
		If checkBox_ShowLog.Checked Then
			Me.Height=Me.Height+listBox_LOG.Height+20
			listBox_LOG.Visible=True
			
		Else
			listBox_LOG.Visible=False
			Me.Height=Me.Height-listBox_LOG.Height-20
		End If
		
	End Sub
	
	
	
	Sub ComboBox_PCKeyPress(sender As Object, e As KeyPressEventArgs)
	e.KeyChar = UCase(e.KeyChar)
		
	End Sub
	
	Sub TextBox_MACKeyPress(sender As Object, e As KeyPressEventArgs)
	e.KeyChar = UCase(e.KeyChar)	
	End Sub

	
	Sub checkBox_ConnectRDP_CheckedChanged(sender As Object, e As EventArgs)
		checkBox_ConnectRDP.Checked=iif(checkBox_ConnectRDP.Checked,True,False)
	End Sub
	'******************************************************
	Function TestConnection
		'******************************************************	
	'-> Для тестироания запускать отдельный процесс	
	TestConnection=False
	PConline=False
	Try 
		If My.Computer.Network.Ping(comboBox_PC.Text) Then
			PConline=True				
			TestConnection=True
		End If
	Catch
	End Try		
	
	If PConline Then
		If checkBox_ConnectRDP.Checked Then
			RunMSTSC(comboBox_PC.Text)
		Else				
			MessageBox.Show("Компьютер "+comboBox_PC.Text+" уже включен",MyProgTitle,MessageBoxButtons.OK,MessageBoxIcon.Asterisk)				
		End If					
	End If
	End Function
	
	
	Sub WakeUpTimerTick(sender As Object, e As EventArgs)
		If WakeUpTimerDownCount<=0 Then
			WakeUpTimer.Stop
			StatusProg=0
			ListBoxTimerStringIndex=-1
			listBox_LOG.Items.Add("Закончилось время ожидания запуска ПК")
			listBox_LOG.SelectedIndex=listBox_LOG.Items.Count-1
		Else
			If ListBoxTimerStringIndex=-1 Then
				listBox_LOG.Items.Add("Ожидайте запуск ПК")
				ListBoxTimerStringIndex=listBox_LOG.Items.Add("|")
				listBox_LOG.SelectedIndex=ListBoxTimerStringIndex
			Else
				If PConline Then
					WakeUpTimerDownCount=0
					Exit Sub
				End If
				listBox_LOG.Items.Item(ListBoxTimerStringIndex)=listBox_LOG.Items.Item(ListBoxTimerStringIndex)+"|"
				If WakeUpTestIntervalDownCount<=0 Then
					WakeUpTestIntervalDownCount=WakeUpTestInterval*1000
					TestConnection()
				End If
			End If			
			WakeUpTimerDownCount=WakeUpTimerDownCount-WakeUpTimer.Interval
			WakeUpTestIntervalDownCount=WakeUpTestIntervalDownCount-WakeUpTimer.Interval
		End If
	End Sub
	
	Sub Label4Click(sender As Object, e As EventArgs)
		
	End Sub
	
	Sub Button_editClick(sender As Object, e As EventArgs)		
		editMAC.ShowDialog(Me)
		
		comboBox_PC.Text=PC_name
		textBox_MAC.Text=PC_MAC
	End Sub
End Class
