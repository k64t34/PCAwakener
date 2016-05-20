'
' Сделано в SharpDevelop.
' Пользователь: skorik
' Дата: 23.04.2015
' Время: 13:27
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'

Imports System.Data.SQLite

Public Partial Class editMAC
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
		
	End Sub
	Sub editMACLoad(sender As Object, e As EventArgs)
		MAC.Text=MainForm.PC_MAC
		PC.Text=MainForm.comboBox_PC.Text
	End Sub
	
	Sub Button2Click(sender As Object, e As EventArgs)
	Close		
	End Sub
	
	Sub Button1Click(sender As Object, e As EventArgs)
		Dim myConnection As SQLiteConnection = New SQLiteConnection()
		Try
			myConnection.ConnectionString = "Data Source="+MainForm.MAC_DB+";"
			myConnection.Open()		
		Catch ex As Exception		
			MsgBox ("Не удалось открыть БД MAC адресов",vbOKOnly+vbCritical,"Ошибка")
		End Try	
		Try		
			Dim SQLcomand As New SQLiteCommand
			SQLcomand=myConnection.CreateCommand
			SQLcomand.CommandText="INSERT OR REPLACE INTO MAC (pc,mac) VALUES('"+PC.Text+"','"+MAC.Text+"')"
			SQLcomand.ExecuteNonQuery()
			
			MainForm.comboBox_PC.Text=PC.Text
		    MainForm.PC_MAC=MAC.Text
			
		Catch er As Exception	
			MsgBox ("Не удалось записать изменения",vbOKOnly+vbCritical,"Ошибка")
		End try
		myConnection.Close	
		Close
		
	End Sub
End Class
