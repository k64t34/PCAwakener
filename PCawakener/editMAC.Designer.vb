'
' Сделано в SharpDevelop.
' Пользователь: skorik
' Дата: 23.04.2015
' Время: 13:27
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Partial Class editMAC
	Inherits System.Windows.Forms.Form
	
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
		Me.button1 = New System.Windows.Forms.Button()
		Me.button2 = New System.Windows.Forms.Button()
		Me.PC = New System.Windows.Forms.TextBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.label2 = New System.Windows.Forms.Label()
		Me.MAC = New System.Windows.Forms.MaskedTextBox()
		Me.SuspendLayout
		'
		'button1
		'
		Me.button1.Location = New System.Drawing.Point(53, 175)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(75, 23)
		Me.button1.TabIndex = 0
		Me.button1.Text = "Сохранить"
		Me.button1.UseVisualStyleBackColor = true
		AddHandler Me.button1.Click, AddressOf Me.Button1Click
		'
		'button2
		'
		Me.button2.Location = New System.Drawing.Point(309, 175)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(75, 23)
		Me.button2.TabIndex = 1
		Me.button2.Text = "Отмена"
		Me.button2.UseVisualStyleBackColor = true
		AddHandler Me.button2.Click, AddressOf Me.Button2Click
		'
		'PC
		'
		Me.PC.Enabled = false
		Me.PC.Location = New System.Drawing.Point(119, 34)
		Me.PC.Name = "PC"
		Me.PC.Size = New System.Drawing.Size(265, 20)
		Me.PC.TabIndex = 3
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(28, 37)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(85, 23)
		Me.label1.TabIndex = 4
		Me.label1.Text = "Имя ПК"
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(28, 78)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(100, 23)
		Me.label2.TabIndex = 5
		Me.label2.Text = "MAC"
		'
		'MAC
		'
		Me.MAC.Culture = New System.Globalization.CultureInfo("")
		Me.MAC.Location = New System.Drawing.Point(119, 75)
		Me.MAC.Mask = "AA:AA:AA:AA:AA:AA"
		Me.MAC.Name = "MAC"
		Me.MAC.ShortcutsEnabled = false
		Me.MAC.Size = New System.Drawing.Size(122, 20)
		Me.MAC.TabIndex = 7
		Me.MAC.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
		'
		'editMAC
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(438, 241)
		Me.Controls.Add(Me.MAC)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.PC)
		Me.Controls.Add(Me.button2)
		Me.Controls.Add(Me.button1)
		Me.Name = "editMAC"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Редактирование МАК адреса"
		AddHandler Load, AddressOf Me.editMACLoad
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private MAC As System.Windows.Forms.MaskedTextBox
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private PC As System.Windows.Forms.TextBox
	Private button2 As System.Windows.Forms.Button
	Private button1 As System.Windows.Forms.Button
End Class
