Module Module1
    '//////////////////////////////////////////////////
    '****************** AUTOCOMPLETE COMBOBOX SUB **************
    '****************** AUTHOR : KUNAL MUKHERJEE  **************
    '
    'NAME :  Autocomplete for windows form combobox.
    'DECLARATION : I got the basic idea of developing a 
    '         AutoComplete Sub for a combobox from
    '         MSDN and I have edited the code and here is the result. 
    '         This sub is much more  
    '         professional and flexible to use.
    '
    '
    'INPUTS :   The name of the combo box and the event 
    '         argument for the combo box.
    'RETURNS :  Nothing.

    '++++++++ HOW TO USE (simply follow the steps stated below): ++++++++++
    '
    'STEP 1: Add this moudule to your application. 
    '   or you can also copy and paste the 
    '  following sub "AutoComplete" in a existing module 
    '   in your project. But it
    '  is recomended that you add a new module to your project and then 
    '  copy-paste this sub to it.
    '
    'STEP 2: This sub can be used by two ways. If you want 
    '  that you want to restric the 
    '  user to type textx that is not in the list otherwise
    '  you can allow the user
    '  to type texts that are not in the list. for that
    '  follow the NOTE section
    '  in the code of the sub and change it as per your need. however the 
    '  following can be usefull.
    '
    '  if you want to restrict the user to type texts
    '  that are not in the list, then
    '  leave the following code in the sub as it is.
    '  by default if you don't alter
    '  anything in the code then the user will be restricted.
    '
    '  '/////// THREE LINES SECTION STARTS HERE ///////////
    '  cbo.SelectionStart = sActual.Length - 1
    '  cbo.SelectionLength = sActual.Length - 1
    '  cbo.Text = Mid(cbo.Text, 1, Len(cbo.Text) - 1)
    '  '/////// THREE LINES SECTION ENDS HERE /////////////
    '    
    '  and comment this line. by default this line is commentd.
    '  'bMatchFound = True (Commented)
    '
    '  but if yu want to allow the user to type texts
    '  that are not in the list then
    '  comment the following three lines of code in the codes of the sub.
    '
    '  '/////// THREE LINES SECTION STARTS HERE ///////////
    '  'cbo.SelectionStart = sActual.Length - 1
    '  'cbo.SelectionLength = sActual.Length - 1
    '  'cbo.Text = Mid(cbo.Text, 1, Len(cbo.Text) - 1)
    '  '/////// THREE LINES SECTION ENDS HERE /////////////
    '
    '  and uncomment this line.
    '  bMatchFound = True (Uncommented)
    '
    'STEP 3: Call this sub from the KeyUp event of your combobox.
    '
    'STEP 4: Whether your combobox is databound or it
    '  contains normal text list, the sub
    '  will work fine. But it is recomended that you use a DataView to bind the 
    '  combobox to the datasource. Like the following:
    '
    '  here (dbConn) is the connection object.
    '
    '  Dim DATest As New OleDbDataAdapter("SELECT * FROM Persons", dbConn)
    '  DATest.Fill(DSTest, "Names")
    '  DVName = New DataView(DSTest.Tables("Names"))
    '  Me.ComboBox1.DataSource = DVName
    '  Me.ComboBox1.DisplayMember = "Name"
    '
    '  if you have done all the things properly 
    '  then the AutoComplete sub should
    '  work fine and normally. For further queries please contact me at the 
    '  following e-mail id : kunal_programmer@rediffmail.com
    ' +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



    Public Sub AutoComplete(ByVal cbo As ComboBox, _
       ByVal e As System.Windows.Forms.KeyEventArgs)
        ' Call this from your form passing in the name 
        ' of your combobox and the event arg:
        ' AutoComplete(cboState, e)
        Dim iIndex As Integer
        Dim sActual As String
        Dim sFound As String
        Dim bMatchFound As Boolean

        'check if the text is blank or not, if not then only proceed
        If Not cbo.Text = "" Then 'if the text is not blank then only proceed

            ' If backspace then remove the last character 
            ' that was typed in and try to find 
            ' a match. note that the selected text from the 
            ' last character typed in to the 
            ' end of the combo text field will also be deleted.
            If e.KeyCode = Keys.Back Then
                cbo.Text = Mid(cbo.Text, 1, Len(cbo.Text) - 1)
            End If

            ' Do nothing for some keys such as navigation keys...
            If ((e.KeyCode = Keys.Left) Or _
             (e.KeyCode = Keys.Right) Or _
             (e.KeyCode = Keys.Up) Or _
             (e.KeyCode = Keys.Down) Or _
             (e.KeyCode = Keys.PageUp) Or _
             (e.KeyCode = Keys.PageDown) Or _
             (e.KeyCode = Keys.Home) Or _
             (e.KeyCode = Keys.End)) Then
                Return
            End If


            Do
                ' Store the actual text that has been typed.
                sActual = cbo.Text
                ' Find the first match for the typed value.
                iIndex = cbo.FindString(sActual)
                ' Get the text of the first match.
                ' if index > -1 then a match was found.

                If (iIndex > -1) Then '** FOUND SECTION **
                    sFound = cbo.Items(iIndex).ToString()
                    ' Select this item from the list.
                    cbo.SelectedIndex = iIndex
                    ' Select the portion of the text that was automatically
                    ' added so that additional typing will replace it.
                    cbo.SelectionStart = sActual.Length
                    cbo.SelectionLength = sFound.Length
                    bMatchFound = True
                Else '** NOT FOUND SECTION **

                    'if there isn't a match and the text typed in is only 1 character 
                    'or nothing then just select the first entry in the combo box.

                    If sActual.Length = 1 Or sActual.Length = 0 Then
                        cbo.SelectedIndex = 0
                        cbo.SelectionStart = 0
                        cbo.SelectionLength = Len(cbo.Text)
                        bMatchFound = True

                    Else

                        'if there isn't a match for the text typed in then 
                        'remove the last character of the text typed in
                        'and try to find a match.

                        '************************** NOTE **************************
                        'COMMENT THE FOLLOWING THREE LINES AND UNCOMMENT 
                        'THE (bMatchFound = True) LINE 
                        'INCASE YOU WANT TO ALLOW TEXTS TO BE TYPED IN
                        ' WHICH ARE NOT IN THE LIST. ELSE IF 
                        'YOU WANT TO RESTRICT THE USER TO TYPE TEXTS WHICH ARE 
                        'NOT IN THE LIST THEN LEAVE THE FOLLOWING THREE LINES AS IT IS
                        'AND COMMENT THE (bMatchFound = True) LINE.
                        '***********************************************************
                        '/////// THREE LINES SECTION STARTS HERE ///////////
                        cbo.SelectionStart = sActual.Length - 1
                        cbo.SelectionLength = sActual.Length - 1
                        cbo.Text = Mid(cbo.Text, 1, Len(cbo.Text) - 1)
                        '/////// THREE LINES SECTION ENDS HERE /////////////
                        'bMatchFound = True

                    End If

                End If

            Loop Until bMatchFound

        End If

    End Sub
End Module
