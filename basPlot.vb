Option Strict Off
Option Explicit On
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Module Module2

    Public Sub PlotEndviewToBmp(ByRef bmpNm As String, Optional ByRef orient As Boolean = False)
        'Dim ft As New FileTools
        'Dim pngFile As String = ft.FileNameWoExt(bmpNm)
        'pngFile = "tmp_" & pngFile & ".PNG"
        'Dim pngFolder As String = ft.FolderFromFileName(bmpNm)
        'pngFile = pngFolder & pngFile
        Dim fs As New clsSharingViolation
        Dim point1(1) As Double
        Dim point2(1) As Double
        Dim eMin As Object
        Dim eMax As Object
        Dim Media As Object
        Dim mCnt As Integer
        SortByPlotting()
        'Set path location
        'path = AcadDOC.GetVariable("DWGPREFIX") & "PLOTS\"
        'bmpNm = path & bmpNm
        'bmpNm = "c:\temp\plots\" & bmpNm
        'create path
        'CreateDir path
        'get extents
        eMin = AcadDOC.GetVariable("EXTMIN")
        eMax = AcadDOC.GetVariable("EXTMAX")
        point1(0) = eMin(0)
        point1(1) = eMin(1)
        point2(0) = eMax(0) '9.25
        point2(1) = eMax(1) '13.4889
        ' Send information about window to current layout
        AcadDOC.ActiveLayout.SetWindowToPlot(point1, point2)
        ' Read back window information
        AcadDOC.ActiveLayout.GetWindowToPlot(point1, point2)
        ' Make sure we tell the drawing to plot a view, not some other plot style
        AcadDOC.ActiveLayout.PlotType = AutoCAD.AcPlotType.acWindow
        ' Send Plot To Window
        'AcadDOC.Plot.DisplayPlotPreview acFullPreview
        Try
            Media = AcadDOC.ActiveLayout.GetPlotDeviceNames()
            If dimVar(Media) Then
                For mCnt = 0 To UBound(Media)
                    'Debug.Print(UCase(Media(mCnt)))
                    If InStr(1, UCase(Media(mCnt)), "PUBLISHTOWEB PNG.PC3") Then
                        AcadDOC.ActiveLayout.ConfigName = Media(mCnt)
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            Exit Sub
        End Try
        Media = AcadDOC.ActiveLayout.GetCanonicalMediaNames()
        If dimVar(Media) Then
            For mCnt = 0 To UBound(Media)
                'Debug.Print(UCase(Media(mCnt)))
                If InStr(1, UCase(Media(mCnt)), "USERDEFINEDRASTER (802.00 X 290.00PIXELS)") Then 'for endviews
                    'If InStr(1, UCase(Media(mCnt)), "SUPER_VGA_(800.00_X_600.00_PIXELS)") Then 'for endviews
                    AcadDOC.ActiveLayout.CanonicalMediaName = Media(mCnt)
                    Exit For
                End If
                'Debug.Print UCase(Media(mCnt))
            Next
        End If

        With AcadDOC.ActiveLayout
            .RefreshPlotDeviceInfo()
            .ConfigName = "PublishToWeb PNG.pc3"
            .StyleSheet = "monochrome.ctb"
            '.CanonicalMediaName = "Letter"
            .PlotOrigin = (point1)
            '.SetWindowToPlot point1, point2
            .PlotType = AutoCAD.AcPlotType.acExtents
            '.GetWindowToPlot point1, point2
            If orient Then
                .PlotRotation = AutoCAD.AcPlotRotation.ac90degrees
            Else
                .PlotRotation = AutoCAD.AcPlotRotation.ac0degrees
            End If
            .PlotViewportBorders = False
            .PlotViewportsFirst = False
            .PlotWithPlotStyles = True
            .PlotWithLineweights = False
            .PlotHidden = False
            .CenterPlot = True
             .PaperUnits = AutoCAD.AcPlotPaperUnits.acPixels
            .ScaleLineweights = False
            .UseStandardScale = True
            .StandardScale = AutoCAD.AcPlotScale.acScaleToFit
        End With
        AcadDOC.Plot.QuietErrorMode = True
        'tStr = path & "\" & _
        ''        bmpNm & ".bmp"
        AcadDOC.Plot.PlotToFile(bmpNm) 'tStr
        System.Windows.Forms.Application.DoEvents()

        If System.IO.File.Exists(bmpNm) Then
            fs.FileName = bmpNm
            While fs.Run < 1
                System.Windows.Forms.Application.DoEvents()
            End While
            Dim nh As Int32
            Dim nw As Int32
            Dim tmp As Bitmap = Nothing
            Try
                tmp = Bitmap.FromFile(bmpNm) 'create temp to avoid access error
            Catch ex As Exception
                tmp = Nothing
            End Try

            '-------------------------------------------------------------------------------------------------
            Dim temp As New Bitmap(tmp.Width, tmp.Height, PixelFormat.Format32bppArgb) 'the new bitmap
            nw = (tmp.Width * 4)
            nh = (tmp.Height * 4)
            temp.SetResolution(nw, nh)
            Dim g As Graphics = Nothing
            Dim cliptest As Bitmap = Nothing

            Try
                g = Graphics.FromImage(CType(temp, Image))
                g.DrawImage(CType(tmp, Image), 0, 0, tmp.Width, tmp.Height)
                g.Dispose()
                cliptest = temp.Clone
                temp.Dispose()
            Catch ex As Exception
                Debug.Print(ex.ToString)
            End Try

            '-------------------------------------------------------------------------------------------------
            ' Dim cliptest As Bitmap

            cliptest = New Bitmap(tmp) 'tmp 802x290
            tmp.Dispose() 'need to dispose to avoid access error

            Try
                System.IO.File.Delete(bmpNm)
            Catch ex As Exception

            End Try
            Try
                Dim rect As Rectangle
                'resize by 50/%
                nh = Fix(cliptest.Height) '/ 2)
                nw = Fix(cliptest.Width) ' / 2)
                cliptest = ImageResize(cliptest, nw, nh)
                'reduce color bw
                cliptest = ConvertToMonoChromeWhiteBackground(cliptest)
                'get extents
                'rect = TransparentClip(cliptest)
                ''add fuzz---------------------------
                rect.X = 0
                rect.Y = 0
                rect.Width = (nw)
                rect.Height = (nh)
                'final clip
                cliptest = ClipBox(cliptest, rect)
                'clipTest.Save("C:\CipPVtest.tif", Imaging.ImageFormat.Tiff)
                'cliptest = ConvertToMonoChrome(cliptest)
                'make transparent
                cliptest.MakeTransparent(Color.White)

                cliptest.Save(bmpNm, System.Drawing.Imaging.ImageFormat.Png)
                While fs.Run < 1
                    System.Windows.Forms.Application.DoEvents()
                End While
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub PlotToBmp(ByRef bmpNm As String, Optional ByRef orient As Boolean = False)
        'Dim ft As New FileTools
        'Dim pngFile As String = ft.FileNameWoExt(bmpNm)
        'pngFile = "tmp_" & pngFile & ".PNG"
        'Dim pngFolder As String = ft.FolderFromFileName(bmpNm)
        'pngFile = pngFolder & pngFile
        Dim fs As New clsSharingViolation
        Dim point1(1) As Double
        Dim point2(1) As Double
        Dim eMin As Object
        Dim eMax As Object
        Dim Media As Object
        Dim mCnt As Integer
        SortByPlotting()
        'Set path location
        'path = AcadDOC.GetVariable("DWGPREFIX") & "PLOTS\"
        'bmpNm = path & bmpNm
        'bmpNm = "c:\temp\plots\" & bmpNm
        'create path
        'CreateDir path
        'get extents
        eMin = AcadDOC.GetVariable("EXTMIN")
        eMax = AcadDOC.GetVariable("EXTMAX")
        point1(0) = eMin(0)
        point1(1) = eMin(1)
        point2(0) = eMax(0) '9.25
        point2(1) = eMax(1) '13.4889
        ' Send information about window to current layout
        AcadDOC.ActiveLayout.SetWindowToPlot(point1, point2)
        ' Read back window information
        AcadDOC.ActiveLayout.GetWindowToPlot(point1, point2)
        ' Make sure we tell the drawing to plot a view, not some other plot style
        AcadDOC.ActiveLayout.PlotType = AutoCAD.AcPlotType.acWindow
        ' Send Plot To Window
        'AcadDOC.Plot.DisplayPlotPreview acFullPreview
        Try
            Media = AcadDOC.ActiveLayout.GetPlotDeviceNames()
            If dimVar(Media) Then
                For mCnt = 0 To UBound(Media)
                    'Debug.Print(UCase(Media(mCnt)))
                    If InStr(UCase(Media(mCnt)), "PNG") > 0 Then
                        AcadDOC.ActiveLayout.ConfigName = Media(mCnt)
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            Exit Sub
        End Try

        Media = AcadDOC.ActiveLayout.GetCanonicalMediaNames()
        If dimVar(Media) Then
            For mCnt = 0 To UBound(Media)
                '--------------------------------------------------------------------------------------------------------
                'Details
                '--------------------------------------------------------------------------------------------------------
                'Debug.Print(UCase(Media(mCnt)))
                ''If InStr(1, UCase(Media(mCnt)), "SUPER_XGA_(1280.00_X_1024.00_PIXELS)") Then 'for endviews
                'If InStr(UCase(Media(mCnt)), "USERDEFINEDRASTER (1024.00 X 1024.00PIXELS)") > 0 Then 'for details?
                '    AcadDOC.ActiveLayout.CanonicalMediaName = Media(mCnt)
                '    Exit For
                'End If

                '--------------------------------------------------------------------------------------------------------
                'Endviews
                '--------------------------------------------------------------------------------------------------------
                Debug.Print(UCase(Media(mCnt)))
                'If InStr(1, UCase(Media(mCnt)), "USERDEFINEDRASTER (801.00 X 290.00PIXELS)") Then 'for endviews
                '    AcadDOC.ActiveLayout.CanonicalMediaName = Media(mCnt)
                '    Exit For
                'End If
                '--------------------------------------------------------------------------------------------------------
                If InStr(1, UCase(Media(mCnt)), "USERDEFINEDRASTER (750.00 X 1050.00PIXELS)") Then 'for portrait letter
                    AcadDOC.ActiveLayout.CanonicalMediaName = Media(mCnt)
                    Exit For
                End If
                '--------------------------------------------------------------------------------------------------------

            Next
        End If

        With AcadDOC.ActiveLayout
            .RefreshPlotDeviceInfo()
            '.ConfigName = "BmpFile.pc3"
            .StyleSheet = "monochrome.ctb" '"Tiff.ctb"
            '.CanonicalMediaName = "Letter"
            .PlotOrigin = (point1)
            '.SetWindowToPlot point1, point2
            .PlotType = AutoCAD.AcPlotType.acExtents
            '.GetWindowToPlot point1, point2
            If orient Then
                .PlotRotation = AutoCAD.AcPlotRotation.ac90degrees
            Else
                .PlotRotation = AutoCAD.AcPlotRotation.ac0degrees
            End If
            .PlotWithPlotStyles = True
            .PlotWithLineweights = True
            .PlotViewportBorders = False
            .PlotViewportsFirst = False
            .PlotHidden = False
            .CenterPlot = True
            '.CenterPlot = True
            .PaperUnits = AutoCAD.AcPlotPaperUnits.acPixels
            .ScaleLineweights = False 'property disabled for the model space layout
            '.UseStandardScale = True
            .StandardScale = AutoCAD.AcPlotScale.acScaleToFit
        End With

        AcadDOC.Plot.QuietErrorMode = True
        Dim tStr As String = UCase(bmpNm) & ".PNG"
        AcadDOC.Plot.PlotToFile(tStr) 'tStr
        System.Windows.Forms.Application.DoEvents()
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If File.Exists(bmpNm) Then
            fs.FileName = bmpNm
            While fs.Run < 1
                System.Windows.Forms.Application.DoEvents()
            End While

            Dim tmp As Bitmap = dmlo(bmpNm)
            Try
                File.Delete(bmpNm)
            Catch ex As Exception

            End Try
            Try
                tmp.Save(bmpNm, System.Drawing.Imaging.ImageFormat.Png)
                While fs.Run < 1
                    System.Windows.Forms.Application.DoEvents()
                End While
            Catch ex As Exception

            End Try
            tmp.Dispose() 'need to dispose to avoid access error
        End If
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        'tStr = file2String(tStr) : WFR()
        'HoldStr = HoldStr & tStr
        'WFR()
        'DeleteFile path & "\" & AcadDOC.GetVariable("CLAYER") & ".eps"
        'WFR


        'If frmPanel.mnuToolsPlotDefault.Checked = True Then
        'default = "&l0H"
        'Dim tStr As String
        'tStr = file2String(path & "\" & AcadDOC.GetVariable("CLAYER") & ".pcl")
        'tStr = Replace(tStr, Chr(27) & "&l0H", Chr(27) & "&l8H")
        'string2File tStr, "c:\temp\test.pcl"
        'Dim rPlt As String
        'rPlt = "LPR -S" & regGetPrn & _
        ''    " -Praw " & _
        ''    path & "\" & AcadDOC.GetVariable("CLAYER") & ".eps"
        'RunProcess rPlt
        'End If


    End Sub



    Private Function dmlo(ByVal fn As String) As Bitmap
        Dim bm As Bitmap = New Bitmap(fn)
        Dim cliptest As Bitmap = New Bitmap(bm)
        ' Dim fuzz As Int64 = 1
        bm.Dispose()
        ' Dim rect As Rectangle = New Rectangle(42, 0, 938, 1024)
        'Dim fuzz As Int64 = 8
        Try
            'resolution?
            cliptest.SetResolution(72, 72)
            'reduce color bw
            cliptest = ConvertToMonoChrome(cliptest)

            'cliptest = ConvertToMonoChromeWhiteBackground(cliptest)
            'make transparent
            cliptest.MakeTransparent(Color.White)
            'get extents
            'Dim rect As Rectangle
            'rect = TransparentClip(cliptest)
            'add fuzz---------------------------
            'rect.X = rect.X - fuzz
            'rect.Y = rect.Y - fuzz
            'rect.Width = rect.Width + (fuzz * 2)
            'rect.Height = rect.Height + (fuzz * 2)
            'final clip
            'cliptest = ClipBox(cliptest, rect)
            ''clipTest.Save("C:\CipPVtest.tif", Imaging.ImageFormat.Tiff)
        Catch ex As Exception
            cliptest = Nothing
        End Try
        'make transparent
        'clipTest.MakeTransparent(Color.White)
        Return cliptest
    End Function
    Private Function ClipCircle(ByVal srcBitmap As Bitmap, _
        ByVal section As Rectangle, Optional ByVal BackClrTransparent As Boolean = False) As Bitmap
        ' Create the new bitmap and graphics object
        Dim bmp As New Bitmap(Math.Abs(section.Width), Math.Abs(section.Height))
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.Clear(Color.Transparent)
        g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel)
        Dim PhotoProcess As Bitmap
        Dim ng As Graphics
        Dim Circular As GraphicsPath
        Dim nRec As Rectangle = New Rectangle(0, 0, Math.Abs(section.Width), Math.Abs(section.Height))
        ' Dimension image
        PhotoProcess = New Bitmap(Math.Abs(section.Width), Math.Abs(section.Height))
        ng = Graphics.FromImage(PhotoProcess)
        'ng.Clear(Color.Transparent)
        ng.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        ' circle
        Circular = New System.Drawing.Drawing2D.GraphicsPath()
        Circular.AddEllipse(0, 0, Math.Abs(section.Width), Math.Abs(section.Height))
        'ng.DrawPath(Pens.Black, path)
        ng.SetClip(Circular, Drawing2D.CombineMode.Replace)
        ng.DrawImage(bmp, 0, 0, nRec, GraphicsUnit.Pixel)
        ng.DrawPath(New Pen(Color.Black, 4), Circular)
        ' Clean up
        g.Dispose()
        ng.Dispose()
        ' Return the bitmap
        Return PhotoProcess
    End Function

    Private Function ClipBox(ByVal srcBitmap As Bitmap, ByVal section As Rectangle) As Bitmap
        ' Create the new bitmap and graphics object
        Dim bmp As New Bitmap(Math.Abs(section.Width), Math.Abs(section.Height))
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.Clear(Color.Transparent)
        g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel)
        Dim PhotoProcess As Bitmap
        Dim ng As Graphics
        Dim box As GraphicsPath
        Dim nRec As Rectangle = New Rectangle(0, 0, Math.Abs(section.Width), Math.Abs(section.Height))

        ' Dimension image
        PhotoProcess = New Bitmap(Math.Abs(section.Width), Math.Abs(section.Height))

        ng = Graphics.FromImage(PhotoProcess)

        'ng.Clear(Color.Transparent)
        ng.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        ' circle
        box = New System.Drawing.Drawing2D.GraphicsPath()
        Dim bRect As Rectangle = New Rectangle(0, 0, Math.Abs(section.Width), Math.Abs(section.Height))
        box.AddRectangle(bRect)
        ng.SetClip(box, Drawing2D.CombineMode.Replace)
        ng.DrawImage(bmp, 0, 0, nRec, GraphicsUnit.Pixel)
        'add border?
        'ng.DrawPath(New Pen(Color.Black, 4), box)
        ' Clean up
        g.Dispose()
        ng.Dispose()
        ' Return the bitmap
        Return PhotoProcess
    End Function

    'IMAGE RESIZE
    '------------
    'objImage: image to change
    'intWidth: An integer value for the new image width 
    'intHeight : An integer value for the new image Height

    Public Function ImageResize(ByVal objImage As Image _
                         , Optional ByVal intWidth As Integer = 0 _
                         , Optional ByVal intHeight As Integer = 0) As Bitmap
        If intWidth > objImage.Width Then intWidth = objImage.Width
        If intHeight > objImage.Height Then intHeight = objImage.Height
        If intWidth = 0 And intHeight = 0 Then
            intWidth = objImage.Width
            intHeight = objImage.Height
        ElseIf intHeight = 0 And intWidth <> 0 Then
            intHeight = Fix(objImage.Height * intWidth / objImage.Width)
        ElseIf intWidth = 0 And intHeight <> 0 Then
            intWidth = Fix(objImage.Width * intHeight / objImage.Height)
        End If
        Dim imgOutput As New Bitmap(objImage, intWidth, intHeight)
        Dim imgFormat As ImageFormat = objImage.RawFormat
        'imgOutput.Save("c:\test.png", imgFormat)
        objImage.Dispose()
        AcadBitMap = imgOutput.Clone
        imgOutput.Dispose()
        Return AcadBitMap
    End Function

    Public Sub WindowToPlot(ByRef pdfNm As String, Optional ByRef orient As Boolean = False)
        Dim path As String
        Dim point1(1) As Double
        Dim point2(1) As Double
        Dim eMin As Object
        Dim eMax As Object
        Dim Media As Object
        Dim mCnt As Integer
        Dim MediaStr As String
        Dim tStr As String
        SortByPlotting()
        'Set path location
        path = AcadDOC.GetVariable("DWGPREFIX") & "PLOTS"
        'create path
        'CreateDir path
        'get extents
        eMin = AcadDOC.GetVariable("EXTMIN")
        eMax = AcadDOC.GetVariable("EXTMAX")
        point1(0) = eMin(0)
        point1(1) = eMin(1)
        point2(0) = eMax(0) '9.25
        point2(1) = eMax(1) '13.4889
        ' Send information about window to current layout
        AcadDOC.ActiveLayout.SetWindowToPlot(point1, point2)
        ' Read back window information
        AcadDOC.ActiveLayout.GetWindowToPlot(point1, point2)
        ' Make sure we tell the drawing to plot a view, not some other plot style
        AcadDOC.ActiveLayout.PlotType = AutoCAD.AcPlotType.acWindow
        ' Send Plot To Window
        'AcadDOC.Plot.DisplayPlotPreview acFullPreview
        On Error Resume Next
        Media = AcadDOC.ActiveLayout.GetPlotDeviceNames()
        If dimVar(Media) Then
            For mCnt = 0 To UBound(Media)
                If InStr(1, UCase(Media(mCnt)), "SMALLFORMATPS") Then
                    AcadDOC.ActiveLayout.ConfigName = Media(mCnt)
                    Exit For
                End If
            Next
        End If

        Media = AcadDOC.ActiveLayout.GetCanonicalMediaNames()
        If dimVar(Media) Then
            For mCnt = 0 To UBound(Media)
                If InStr(1, UCase(Media(mCnt)), "LETTER") Then
                    AcadDOC.ActiveLayout.CanonicalMediaName = Media(mCnt)
                    Exit For
                End If
            Next
        End If

        With AcadDOC.ActiveLayout
            .RefreshPlotDeviceInfo()
            .ConfigName = "SmallFormatPS" '"\\SVG_ENG\QMS 860+ Level 2"
            .StyleSheet = "hplaser.ctb"
            .CanonicalMediaName = "Letter"
            .PlotOrigin = (point1)
            .SetWindowToPlot(point1, point2)
            .PlotType = AutoCAD.AcPlotType.acWindow
            .GetWindowToPlot(point1, point2)
            If orient Then
                .PlotRotation = AutoCAD.AcPlotRotation.ac90degrees
            Else
                .PlotRotation = AutoCAD.AcPlotRotation.ac0degrees
            End If
            .PlotViewportBorders = False
            .PlotViewportsFirst = False
            .PlotWithLineweights = True
            .PlotWithPlotStyles = True
            .PlotHidden = False
            .CenterPlot = True
            .PaperUnits = AutoCAD.AcPlotPaperUnits.acInches
            .ScaleLineweights = False
            .UseStandardScale = True
            .StandardScale = AutoCAD.AcPlotScale.acScaleToFit
        End With
        AcadDOC.Plot.QuietErrorMode = True

        AcadDOC.Plot.PlotToFile(pdfNm) 'tStr
        WFR()
        'If FileExists(tStr) Then tStr = file2String(tStr): WFR
        'HoldStr = HoldStr & tStr
        'WFR
        'DeleteFile path & "\" & AcadDOC.GetVariable("CLAYER") & ".eps"
        'WFR


        'If frmPanel.mnuToolsPlotDefault.Checked = True Then
        'default = "&l0H"
        'Dim tStr As String
        'tStr = file2String(path & "\" & AcadDOC.GetVariable("CLAYER") & ".pcl")
        'tStr = Replace(tStr, Chr(27) & "&l0H", Chr(27) & "&l8H")
        'string2File tStr, "c:\temp\test.pcl"
        'Dim rPlt As String
        'rPlt = "LPR -S" & regGetPrn & _
        ''    " -Praw " & _
        ''    path & "\" & AcadDOC.GetVariable("CLAYER") & ".eps"
        'RunProcess rPlt
        'End If


    End Sub

    Sub SortByPlotting()
        Dim ACADPref As AutoCAD.AcadDatabasePreferences
        ' Get the user preferences object
        AcadDOC.SetVariable("SORTENTS", 0)
        ACADPref = AcadDOC.Preferences
        ACADPref.ObjectSortByPlotting = True
    End Sub

    'is variant dimensioned
    Function dimVar(ByRef ary As Object) As Boolean
        Dim elements As Short
        elements = 0
        On Error GoTo MemberExit
        If VarType(ary) >= 1892 Then
            elements = UBound(ary)
            If elements >= 0 Then
                dimVar = True
            Else
                dimVar = False
            End If
        End If
        Exit Function
MemberExit:
        dimVar = False
    End Function
End Module