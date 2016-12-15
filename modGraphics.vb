
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.Serialization
Imports System.Resources
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Drawing.Imaging


Module modBlockOuts

    Public Declare Function BitBlt Lib "gdi32.dll" Alias "BitBlt" (ByVal _
                         hdcDest As IntPtr, ByVal nXDest As Int32, ByVal nYDest As  _
                         Int32, ByVal nWidth As Int32, ByVal nHeight As Int32, ByVal _
                         hdcSrc As IntPtr, ByVal nXSrc As Int32, ByVal nYSrc As Int32, _
                         ByVal dwRop As System.Int32) As Int32

    Public Declare Function GetDesktopWindow Lib "user32" () As Int32
    Public Declare Function GetDC Lib "user32" (ByVal hwnd As Int32) As Int32
    Public Declare Function StretchBlt Lib "gdi32" (ByVal hdc As IntPtr, ByVal x As Int32, ByVal y As Int32, ByVal nWidth As Int32, ByVal nHeight As Int32, ByVal hSrcDC As Int32, ByVal xSrc As Int32, ByVal ySrc As Int32, ByVal nSrcWidth As Int32, ByVal nSrcHeight As Int32, ByVal dwRop As Int32) As Int32
    Public Declare Function ReleaseDC Lib "user32" (ByVal hwnd As Int32, ByVal hdc As Int32) As Int32
    Public Const SRCCOPY As Int32 = &HCC0020

    Public Declare Function IntersectRect Lib "user32" (ByRef lpDestRect As RECT, ByRef lpSrc1Rect As RECT, _
        ByRef lpSrc2Rect As RECT) As Integer

    Public Declare Function AddFontResource Lib "gdi32" Alias "AddFontResourceA" (ByVal lpFileName As String) As Int32

    <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
    Private Function Rectangle(ByVal hdc As IntPtr, _
    ByVal ulCornerX As Integer, ByVal ulCornerY As Integer, ByVal lrCornerX As Integer, _
    ByVal lrCornerY As Integer) As Boolean
    End Function

    Public Sub GetHdcForGDI(ByVal e As PaintEventArgs)
        ' Create pen. 
        Dim redPen As New Pen(Color.Red, 1)
        ' Draw rectangle with GDI+. 
        e.Graphics.DrawRectangle(redPen, 10, 10, 100, 50)
        ' Get handle to device context.
        Dim hdc As IntPtr = e.Graphics.GetHdc()
        ' Draw rectangle with GDI using default pen. 
        Rectangle(hdc, 10, 70, 110, 120)
        ' Release handle to device context. 
        e.Graphics.ReleaseHdc(hdc)
    End Sub


    <StructLayout(LayoutKind.Sequential)> _
    Public Structure RECT
        Public left As Int32
        Public top As Int32
        Public right As Int32
        Public bottom As Int32
    End Structure


#Region "Properties"



    Public AcadBitMap As Bitmap
    


 

  

    Public Function checkDuplicate(ByVal Arlst As ArrayList) As Boolean
        checkDuplicate = False
        Dim Htble As Hashtable = New Hashtable
        Dim i As Integer
        For i = 0 To Arlst.Count - 1
            Try
                'Adding ArrayList items into Hashtable
                Htble.Add(Arlst.Item(i), Arlst.Item(i))
                'If any duplicate added to the hashtable then catch exception
            Catch ex As Exception
                'Function will return false if ArrayList contains duplicate
                Return False
            Finally
                'Finally if arraylst doesn't have duplicate then return true
                If Not checkDuplicate = False Then
                    checkDuplicate = True
                End If
            End Try
        Next
    End Function

    Public Function RemSpaces(ByVal str As String) As String
rep:
        str = Replace(str, " ", "")
        If InStr(str, " ") > 0 Then
            GoTo rep
        End If
        If str Is Nothing Then
            Return ""
        Else
            Return str
        End If
    End Function

#End Region

#Region "Ckey"
    Public Function acAddResourceBlk(ByVal BlkName As String, ByVal ExtName As String) As String
        Dim int As Integer = 0
        Dim wf As New FileTools
        Dim fTest As New clsSharingViolation
        Dim dwgName As String
        Dim rdr As Object = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        wf.CreateFolder(Application.StartupPath & "\Blocks")
        dwgName = BlkName & ".dwg"
        dwgName = Application.StartupPath & "\Blocks\" & dwgName
        If File.Exists(dwgName) Then
            Cursor.Current = System.Windows.Forms.Cursors.Default
            Return dwgName
            Exit Function
        End If
        fTest.FileName = dwgName
        'If Not File.Exists(BlkName) Then
        Select Case ExtName
            Case "EB"
                Dim rm As New ResourceManager("Fabcad.DesignSurface.ElevationBlocks", _
                    [Assembly].GetExecutingAssembly())
                rdr = rm.GetObject(BlkName & "." & ExtName)
            Case "EP"
                Dim rm As New ResourceManager("Fabcad.DesignSurface.ErectionBlocks", _
                    [Assembly].GetExecutingAssembly())
                rdr = rm.GetObject(BlkName & "." & ExtName)
            Case "PL"

            Case "CN"

            Case "PB"
                Dim rm As New ResourceManager("Fabcad.DesignSurface.PlanBlocks", _
                [Assembly].GetExecutingAssembly())
                rdr = rm.GetObject(BlkName & "." & ExtName)
            Case Else
        End Select
        Dim fs As New FileStream(dwgName, FileMode.Create)
        Dim w As New BinaryWriter(fs)
        Dim i As Integer
        For i = 0 To UBound(rdr)
            w.Write(rdr(i))
        Next i
        w.Close()
        fs.Close()
        'End If
        While fTest.Run < 1
            System.Windows.Forms.Application.DoEvents()
        End While
        If File.Exists(dwgName) Then
            Return dwgName
        Else
            Return Nothing
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
#End Region

#Region "Misc Methods"

    'Public Function ConvertToMonoChrome(ByVal orig As Bitmap) As Bitmap
    '    'lock the bits of the original bitmap
    '    Dim bmdo As BitmapData = orig.LockBits(New Rectangle(0, 0, orig.Width, orig.Height), ImageLockMode.ReadOnly, orig.PixelFormat)
    '    'and the new 1bpp bitmap
    '    Dim bm As Bitmap = New Bitmap(orig.Width, orig.Height, PixelFormat.Format1bppIndexed)
    '    Dim bmdn As BitmapData = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed)
    '    'for diagnostics
    '    'Dim dt As DateTime = DateTime.Now

    '    'scan through the pixels Y by X
    '    Dim y As Integer
    '    For y = 0 To orig.Height - 1
    '        Dim x As Integer
    '        For x = 0 To orig.Width - 1
    '            'generate the address of the colour pix el
    '            Dim index As Integer = y * bmdo.Stride + x * 4
    '            'check its brightness
    '            'Dim clr As Color = Color.FromArgb(Marshal.ReadByte(bmdo.Scan0, index + 2), Marshal.ReadByte(bmdo.Scan0, index + 1), Marshal.ReadByte(bmdo.Scan0, index))
    '            'Debug.Print(clr.Name)
    '            'Dim val As Int32 = Marshal.ReadByte(bmdo.Scan0, index + 2)
    '            If Color.FromArgb(Marshal.ReadByte(bmdo.Scan0, index + 2), Marshal.ReadByte(bmdo.Scan0, index + 1), Marshal.ReadByte(bmdo.Scan0, index)).GetBrightness() > 0.5F Then
    '                'If val <> 0 Then
    '                SetIndexedPixel(x, y, bmdn, True) 'set it if its bright.
    '                'Else
    '                '    SetIndexedPixel(x, y, bmdn, False) 'set it if its bright
    '                'Debug.Print(clr.Name)
    '                'Else
    '                'Debug.Print(clr.br)
    '                'If clr.Name = "ff000000" Then
    '                'Me.SetIndexedPixel(x, y, bmdn, True) 'set it if its bright.
    '                'End If
    '            End If
    '        Next x
    '    Next y
    '    'tidy up
    '    bm.UnlockBits(bmdn)
    '    orig.UnlockBits(bmdo)
    '    'show the time taken to do the conversion
    '    'Dim ts As TimeSpan = dt.Subtract(DateTime.Now)
    '    'System.Diagnostics.Trace.WriteLine(("Conversion time was:" + ts.ToString()))
    '    'display the 1bpp image.
    '    ConvertToMonoChrome = bm
    'End Function

    Public Function ConvertToMonoChromeWhiteBackground(ByVal orig As Bitmap) As Bitmap
        Dim tempbmp As Bitmap
        tempbmp = New Bitmap(orig)
        ' Set each pixel 
        Dim Xcount As Int32
        Dim pClr As New Color
        For Xcount = 0 To tempbmp.Width - 1
            Dim Ycount As Int32
            For Ycount = 0 To tempbmp.Height - 1
                pClr = tempbmp.GetPixel(Xcount, Ycount)
                If pClr.R <> 255 And pClr.G <> 255 And pClr.B <> 255 Then
                    If pClr.R <> 0 And pClr.G <> 0 And pClr.B <> 0 Then
                        tempbmp.SetPixel(Xcount, Ycount, Color.Black)
                    End If
                End If
            Next Ycount
        Next Xcount
        ConvertToMonoChromeWhiteBackground = tempbmp
    End Function


    Public Function ConvertToMonoChrome(ByVal orig As Bitmap) As Bitmap

        'lock the bits of the original bitmap
        Dim bmdo As BitmapData = orig.LockBits(New Rectangle(0, 0, orig.Width, orig.Height), ImageLockMode.ReadOnly, orig.PixelFormat)
        'and the new 1bpp bitmap
        Dim bm As Bitmap = New Bitmap(orig.Width, orig.Height, PixelFormat.Format1bppIndexed)
        Dim bmdn As BitmapData = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed)
        'for diagnostics
        'Dim dt As DateTime = ConvertCultureDateToUS
        Dim ds As Single = 73
        ds = (ds / 100)
        'scan through the pixels Y by X
        Dim y As Integer
        For y = 0 To orig.Height - 1
            Dim x As Integer
            For x = 0 To orig.Width - 1
                'generate the address of the colour pix el
                Dim index As Integer = y * bmdo.Stride + x * 4
                'check its brightness
                'Dim clr As Color = Color.FromArgb(Marshal.ReadByte(bmdo.Scan0, index + 2), Marshal.ReadByte(bmdo.Scan0, index + 1), Marshal.ReadByte(bmdo.Scan0, index))
                'Debug.Print(clr.Name)
                'Dim val As Int32 = Marshal.ReadByte(bmdo.Scan0, index + 2)
                If Color.FromArgb(Marshal.ReadByte(bmdo.Scan0, index + 2), Marshal.ReadByte(bmdo.Scan0, index + 1), Marshal.ReadByte(bmdo.Scan0, index)).GetBrightness() > ds Then
                    'If val <> 0 Then
                    SetIndexedPixel(x, y, bmdn, True) 'set it if its bright.
                    'Else
                    '    SetIndexedPixel(x, y, bmdn, False) 'set it if its bright
                    'Debug.Print(clr.Name)
                    'Else
                    'Debug.Print(clr.br)
                    'If clr.Name = "ff000000" Then
                    'Me.SetIndexedPixel(x, y, bmdn, True) 'set it if its bright.
                    'End If
                End If
            Next x
        Next y
        'tidy up
        bm.UnlockBits(bmdn)
        orig.UnlockBits(bmdo)
        orig.Dispose()
        bmdn = Nothing
        bmdo = Nothing
        'show the time taken to do the conversion
        'Dim ts As TimeSpan = dt.Subtract(ConvertCultureDateToUS)
        'System.Diagnostics.Trace.WriteLine(("Conversion time was:" + ts.ToString()))
        'display the 1bpp image.
        AcadBitMap = bm.Clone
        bm.Dispose()
        Return AcadBitMap
    End Function

    Public Function TransparentClip(ByVal orig As Bitmap) As Rectangle
        Dim xArr() As String = Nothing
        Dim yArr() As String = Nothing
        Dim xCnt, yCnt As Int32
        Dim xVal, yVal As String
        'lock the bits of the original bitmap
        Dim bmdo As BitmapData = orig.LockBits(New Rectangle(0, 0, orig.Width, orig.Height), ImageLockMode.ReadOnly, orig.PixelFormat)
        'and the new 1bpp bitmap
        Dim bm As Bitmap = New Bitmap(orig.Width, orig.Height, PixelFormat.Format1bppIndexed)
        Dim bmdn As BitmapData = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed)
        'for diagnostics
        Dim dt As DateTime = DateTime.Now
        Dim rect As Rectangle = New Rectangle(0, 0, orig.Width, orig.Height)
        'scan through the pixels Y by X
        Dim y As Integer
        For y = 0 To orig.Height - 1
            Dim x As Integer
            For x = 0 To orig.Width - 1
                'generate the address of the colour pix el
                Dim index As Integer = y * bmdo.Stride + x * 4
                'check its brightness
                If Color.FromArgb(Marshal.ReadByte(bmdo.Scan0, index + 2), Marshal.ReadByte(bmdo.Scan0, index + 1), Marshal.ReadByte(bmdo.Scan0, index)).GetBrightness() < 1.0F Then
                    If xArr Is Nothing Then
                        ReDim Preserve xArr(0)
                        xArr(0) = Format(x, "000000")
                    End If
                    Dim objX As Object = Nothing

                    objX = Filter(xArr, Format(x, "000000"), True)
                    If dimVar(objX) = False Then
                        xVal = Format(x, "000000")
                        ReDim Preserve xArr(xCnt) : xArr(xCnt) = xVal
                        xCnt = (xCnt + 1)
                    End If
                    If yArr Is Nothing Then
                        ReDim Preserve yArr(0)
                        yArr(0) = Format(y, "000000")
                    End If
                    Dim objY As Object = Nothing
                    objY = Filter(yArr, Format(y, "000000"), True)
                    If dimVar(objY) = False Then
                        yVal = Format(y, "000000")
                        ReDim Preserve yArr(yCnt) : yArr(yCnt) = yVal
                        yCnt = (yCnt + 1)
                    End If
                End If
            Next x
        Next y
        'tidy up
        bm.UnlockBits(bmdn)
        orig.UnlockBits(bmdo)
        'show the time taken to do the conversion
        Dim ts As TimeSpan = dt.Subtract(DateTime.Now)
        Debug.WriteLine("Transparent Clip Elapsed Time: " & ts.ToString)
        'System.Diagnostics.Trace.WriteLine(("Conversion time was:" + ts.ToString()))
        'display the 1bpp image.
        Dim sa As New SortArray
        sa.qSort(xArr, 0, UBound(xArr))
        sa.qSort(yArr, 0, UBound(yArr))
        rect.X = Convert.ToDecimal(xArr(0)) - 1
        rect.Y = Convert.ToDecimal(yArr(0)) - 1
        rect.Width = (Convert.ToDecimal(xArr(UBound(xArr))) - rect.X) + 2
        rect.Height = (Convert.ToDecimal(yArr(UBound(yArr))) - rect.Y) + 2
        If rect.X < 0 Then rect.X = 0
        If rect.Y < 0 Then rect.Y = 0
        TransparentClip = rect
    End Function

    'Error: Converting Properties 
    Private Sub SetIndexedPixel(ByVal x As Integer, ByVal y As Integer, ByVal bmd As BitmapData, ByVal pixel As Boolean)
        Dim index As Integer = y * bmd.Stride + (x >> 3)
        Dim p As Byte = Marshal.ReadByte(bmd.Scan0, index)
        Dim mask As Byte = &H80 >> (x And &H7)
        If pixel Then
            p = p Or mask
        Else
            p = p And CByte(mask ^ &HFF)
        End If
        Marshal.WriteByte(bmd.Scan0, index, p)
    End Sub 'SetIndexedPixel

    'Error: Converting Properties 
    Private Sub SetIndexedPixel2(ByVal x As Integer, ByVal y As Integer, ByVal bmd As BitmapData, ByVal pixel As Boolean)
        Dim index As Integer = y * bmd.Stride + (x >> 3)
        Dim p As Byte = Marshal.ReadByte(bmd.Scan0, index)
        Dim mask As Byte = &H80 >> (x And &H7)
        If pixel Then
            p = p Or mask
        Else
            p = p And CByte(mask ^ &HFF)
        End If
        Marshal.WriteByte(bmd.Scan0, index, p)
    End Sub 'SetIndexedPixel

    Public Function GenerateThumbnailPercent(ByVal original As Image, ByVal percentage As Integer) As Image
        If percentage < 1 Then
            Throw New Exception("Thumbnail size must be aat least 1% of the original size")
        End If
        Dim tn As New Bitmap(CInt(original.Width * 0.01F * percentage), _
                             CInt(original.Height * 0.01F * percentage))
        Dim g As Graphics = Graphics.FromImage(tn)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear 'experiment with this...
        g.DrawImage(original, New Rectangle(0, 0, tn.Width, tn.Height), _
                    0, 0, original.Width, original.Height, GraphicsUnit.Pixel)
        g.Dispose()
        Return CType(tn, Image)
    End Function 'GenerateThumbnail

    Public Function GenerateThumbnailDecimal(ByVal original As Image, ByVal percentage As Decimal) As Image
        If percentage < 0 Then
            Throw New Exception("Thumbnail size must be aat least 1% of the original size")
        End If
        Dim tn As New Bitmap(CInt(original.Width * percentage), _
                             CInt(original.Height * percentage))
        Dim g As Graphics = Graphics.FromImage(tn)
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear 'experiment with this...
        g.DrawImage(original, New Rectangle(0, 0, tn.Width, tn.Height), _
                    0, 0, original.Width, original.Height, GraphicsUnit.Pixel)
        g.Dispose()
        Return CType(tn, Image)
    End Function 'GenerateThumbnail


    Public Sub HelpMe()
        SendKeys.Send("{F1}")
    End Sub
    'is object dimensioned
    Public Function dimVar(ByVal ary As Object) As Boolean
        Dim elements As Integer
        elements = 0
        On Error GoTo MemberExit
        If Not ary Is Nothing Then
            elements = UBound(ary)
            If elements >= 0 Then
                dimVar = True
            Else
                dimVar = False
            End If
        Else
            dimVar = False
        End If
        Err.Clear()
        Exit Function
MemberExit:
        dimVar = False
        Err.Clear()
    End Function
#End Region

#Region "Fonts"

    Public Function GetStandardFont(ByVal sz As Single) As Font
        Dim fName As String = Application.StartupPath & "\FAN.ttf"
        If File.Exists(fName) Then
            Dim FabcadPrivateFonts As System.Drawing.Text.PrivateFontCollection
            Dim FabcadFont As Font = Nothing
            FabcadPrivateFonts = New System.Drawing.Text.PrivateFontCollection()
            'add font to private font collection
            'Note This font is stored in a file in the app's bin directory
            'this does not exist on the system
            FabcadPrivateFonts.AddFontFile(filename:=fName)
            'create a font object from the font family in the private collection
            FabcadFont = New Font(family:=FabcadPrivateFonts.Families(0), emSize:=sz, style:=FontStyle.Regular)
            'change the label's font property to use the new font
            Return FabcadFont
        Else
            Return Nothing
        End If
    End Function
#End Region

 

End Module
