Public Class SortArray
    Public Sub qSort(ByVal vArray As Object, ByVal l As Int32, ByVal r As Int32)

        Dim i As Int32
        Dim j As Int32
        Dim X As Object
        Dim Y As Object

        i = l
        j = r
        X = vArray((l + r) / 2)

        While (i <= j)
            While (vArray(i) < X And i < r)
                i = i + 1
            End While

            While (X < vArray(j) And j > l)
                j = j - 1
            End While

            If (i <= j) Then
                Y = vArray(i)
                vArray(i) = vArray(j)
                vArray(j) = Y
                i = i + 1
                j = j - 1
            End If
        End While
        If (l < j) Then qSort(vArray, l, j)
        If (i < r) Then qSort(vArray, i, r)
    End Sub

    Public Function UniqueElevs(ByVal inArray() As String) As Array
        Dim qCnt As Int32
        Dim tmp As Int32
        Dim tmp2 As Int32
        Dim cntVal As Int32
        Dim cntDT As Double
        Dim strChk As String
        Dim retArray() As String = Nothing
        Dim hldArray() As String = Nothing
        Dim tmpArray() As String = Nothing
        If Not inArray Is Nothing Then
            retArray = inArray
            While tmp <= UBound(inArray)
                'by width
                On Error GoTo catchErr
                strChk = Split(retArray(0), "|")(0)
                'strChk = retArray(0)
                'return nonmatching
                retArray = Filter(retArray, strChk, False)
                'return matching
                hldArray = Filter(inArray, strChk, True)
                If tmp = 0 Then tmp = 1
                tmp = tmp + UBound(hldArray)
                'add numbers
                On Error Resume Next
                ReDim Preserve tmpArray(qCnt)
                tmpArray(qCnt) = hldArray(0) '& "|" & UBound(hldArray) + 1
                ''Debug.WriteLine(tmpArray(qCnt))
                qCnt = qCnt + 1
            End While
catchErr:
            If tmpArray Is Nothing Then
                Return Nothing
                Exit Function
            End If
        End If
        Return tmpArray
    End Function

    Public Function unique(ByVal inArray() As String) As Array
        Dim qCnt As Int32
        Dim tmp As Int32
        Dim tmp2 As Int32
        Dim cntVal As Int32
        Dim cntDT As Double
        Dim strChk As String
        Dim retArray() As String = Nothing
        Dim hldArray() As String = Nothing
        Dim tmpArray() As String = Nothing
        If Not inArray Is Nothing Then
            retArray = inArray
            While tmp <= UBound(inArray)
                On Error GoTo catchErr
                'strChk = Split(retArray(0), "|")(0)
                strChk = retArray(0)
                'return nonmatching
                retArray = Filter(retArray, strChk, False)
                'return matching
                hldArray = Filter(inArray, strChk, True)
                If tmp = 0 Then tmp = 1
                tmp = tmp + UBound(hldArray)
                'add numbers
                On Error Resume Next
                ReDim Preserve tmpArray(qCnt)
                tmpArray(qCnt) = hldArray(0) '& "|" & UBound(hldArray) + 1
                ''Debug.WriteLine(tmpArray(qCnt))
                qCnt = qCnt + 1
            End While

catchErr:
            If tmpArray Is Nothing Then
                Return Nothing
                Exit Function
            End If
        End If
        Return tmpArray
    End Function

    Public Function cntarray(ByVal inArray() As String, Optional ByVal Seperator As String = "|") As Array
        Dim qCnt As Int32
        Dim tmp As Int32
        Dim tmp2 As Int32
        Dim cntVal As Int32
        Dim cntDT As Double
        Dim strChk As String
        Dim retArray() As String = Nothing
        Dim hldArray() As String = Nothing
        Dim tmpArray() As String = Nothing
        If Not inArray Is Nothing Then
            retArray = inArray
            While tmp <= UBound(inArray)
                ''Debug.WriteLine(UBound(retArray))
                'by width
                On Error GoTo catchErr
                'strChk = Split(retArray(0), "|")(2)
                strChk = retArray(0)
                'return nonmatching
                retArray = Filter(retArray, strChk, False)
                'return matching
                hldArray = Filter(inArray, strChk, True)
                If tmp = 0 Then tmp = 1
                tmp = tmp + UBound(hldArray)
                'add numbers
                On Error Resume Next
                ReDim Preserve tmpArray(qCnt)
                tmpArray(qCnt) = hldArray(0) & Seperator & UBound(hldArray) + 1
                ''Debug.WriteLine(tmpArray(qCnt))
                qCnt = qCnt + 1
            End While
catchErr:
            If tmpArray Is Nothing Then
                Return Nothing
                Exit Function
            End If
        End If
        Return tmpArray
    End Function

    Public Function vbQSR(ByVal inArray As Object) As Object
        Dim iCnt As Int32
        Dim oCnt As Int32
        Dim tmpArray() As String = Nothing
        ReDim Preserve tmpArray(UBound(inArray))
        oCnt = UBound(inArray)
        'sort the array
        qSort(inArray, 0, UBound(inArray))
        For iCnt = 0 To UBound(inArray) 'To 0
            tmpArray(oCnt) = inArray(iCnt)
            ''Debug.WriteLine(tmpArray(oCnt))
            oCnt = oCnt - 1
        Next
        vbQSR = tmpArray
    End Function

    Public Function AddLeadingZeros(ByVal inArray As Object, _
                                        ByVal FormatZero As String, _
                                        ByVal Delimiter As String, _
                                        ByVal FieldNumber As Int32) As Object
        Dim iCnt As Int32
        Dim oCnt As Int32
        Dim newStr As String = Nothing
        Dim tmpArray() As String = Nothing
        ReDim Preserve tmpArray(UBound(inArray))

        oCnt = UBound(inArray)
        'sort the array
        qSort(inArray, 0, UBound(inArray))
        For iCnt = 0 To UBound(inArray)
            newStr = inArray(iCnt)
            If InStr(newStr, Delimiter) > 0 Then
                Try
                    Dim tmpStr As String = ""
                    Dim chk As Object = Split(newStr, Delimiter)
                    If dimVar(chk) Then
                        For oCnt = 0 To UBound(chk)
                            If oCnt = FieldNumber Then
                                chk(oCnt) = Format(Convert.ToSingle(chk(oCnt)), FormatZero)
                            End If
                            If oCnt = UBound(chk) Then
                                tmpStr = tmpStr & chk(oCnt)
                            Else
                                tmpStr = tmpStr & chk(oCnt) & Delimiter
                            End If
                        Next
                    End If
                    inArray(iCnt) = tmpStr
                Catch ex As Exception
                    inArray = Nothing
                End Try
            Else
                inArray(iCnt) = newStr
            End If
        Next
        AddLeadingZeros = inArray
    End Function

    Public Function RemoveLeadingZeros(ByVal inArray As Object, _
                                    ByVal Delimiter As String, _
                                    ByVal FieldNumber As Int32) As Object
        Dim iCnt As Int32
        Dim oCnt As Int32
        Dim newStr As String = Nothing
        Dim tmpArray() As String = Nothing
        ReDim Preserve tmpArray(UBound(inArray))
        oCnt = UBound(inArray)
        'sort the array
        qSort(inArray, 0, UBound(inArray))
        For iCnt = 0 To UBound(inArray)
            newStr = inArray(iCnt)
            If InStr(newStr, Delimiter) > 0 Then
                Try
                    Dim tmpStr As String = ""
                    Dim chk As Object = Split(newStr, Delimiter)
                    If dimVar(chk) Then
                        For oCnt = 0 To UBound(chk)
                            If oCnt = FieldNumber Then
                                Dim editStr As String = chk(oCnt)
                                editStr = editStr.TrimStart("0")
                                chk(oCnt) = editStr
                            End If
                            If oCnt = UBound(chk) Then
                                tmpStr = tmpStr & chk(oCnt)
                            Else
                                tmpStr = tmpStr & chk(oCnt) & Delimiter
                            End If
                        Next
                    End If
                    inArray(iCnt) = tmpStr
                Catch ex As Exception
                    inArray = Nothing
                End Try
            Else
                inArray(iCnt) = newStr
            End If
        Next
        RemoveLeadingZeros = inArray
    End Function

    'is object dimensioned
    Private Function dimVar(ByVal ary As Object) As Boolean
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

#Region "Points"

    Public Function UniquePoints(ByVal inArray() As Point) As Array
        Dim qCnt As Int32
        Dim tmp As Int32
        Dim tmp2 As Int32
        Dim cntVal As Int32
        Dim cntDT As Double
        Dim strChk As String
        Dim inArrayPoints() As String = Nothing
        Dim retArray() As String = Nothing
        Dim hldArray() As String = Nothing
        Dim tmpArray() As String = Nothing
        If Not inArray Is Nothing Then
            For tmp = 0 To UBound(inArray)
                ReDim Preserve inArrayPoints(tmp)
                inArrayPoints(tmp) = Format(inArray(tmp).X, "0000000") & "," & Format(inArray(tmp).Y, "0000000")
            Next
        End If
        If Not inArrayPoints Is Nothing Then
            retArray = inArrayPoints
            tmp = 0
            While tmp <= UBound(inArrayPoints)
                On Error GoTo catchErr
                'strChk = Split(retArray(0), "|")(0)
                strChk = retArray(0)
                'return nonmatching
                retArray = Filter(retArray, strChk, False)
                'return matching
                hldArray = Filter(inArrayPoints, strChk, True)
                If tmp = 0 Then tmp = 1
                tmp = tmp + UBound(hldArray)
                'add numbers
                On Error Resume Next
                ReDim Preserve tmpArray(qCnt)
                tmpArray(qCnt) = hldArray(0) '& "|" & UBound(hldArray) + 1
                ''Debug.WriteLine(tmpArray(qCnt))
                qCnt = qCnt + 1
            End While
catchErr:
            If tmpArray Is Nothing Then
                Return Nothing
                Exit Function
            Else
                tmp = 0
                For tmp = 0 To UBound(tmpArray)
                    ReDim Preserve inArray(tmp)
                    inArray(tmp).X = Split(tmpArray(tmp), ",")(0)
                    inArray(tmp).Y = Split(tmpArray(tmp), ",")(1)
                Next
            End If
        End If
        Return inArray
    End Function

    Public Function RemovePointFromArray(ByVal p1 As Point, ByVal inArray() As Point) As Array
        Dim i, o As Int32
        Dim Outarray() As Point = Nothing
        If dimVar(inArray) Then
            For i = 0 To UBound(inArray)
                If inArray(i) <> p1 Then
                    ReDim Preserve Outarray(o)
                    Outarray(o) = inArray(i)
                    o = (o + 1)
                End If
            Next
        End If
        Return Outarray
    End Function

    Public Function IsPointInArray(ByVal p1 As Point, ByVal inArray() As Point) As Boolean
        Dim i As Int32
        Dim bool As Boolean
        If dimVar(inArray) Then
            For i = 0 To UBound(inArray)
                If inArray(i) = p1 Then
                    bool = True
                    Exit For
                End If
            Next
        End If
        Return bool
    End Function

    Public Function RetPointsBetweenXs(ByVal p1 As Point, ByVal p2 As Point, ByVal inArray() As Point) As Array
        Dim i, o As Int32
        Dim Outarray() As Point = Nothing
        If dimVar(inArray) Then
            For i = 0 To UBound(inArray)
                If inArray(i).X > p1.X And inArray(i).X < p2.X Then
                    ReDim Preserve Outarray(o)
                    Outarray(o) = inArray(i)
                    o = (o + 1)
                End If
            Next
        End If
        Return Outarray
    End Function

    Public Function retMinMax(ByVal inArray() As Point) As Array
        Dim i As Int32
        Dim px() As Int32 = Nothing
        Dim py() As Int32 = Nothing
        Dim ptMinMax(1) As Point
        Dim ptMin As Point
        Dim ptMax As Point
        If dimVar(inArray) Then
            For i = 0 To UBound(inArray)
                ReDim Preserve px(i)
                px(i) = inArray(i).X
                ReDim Preserve py(i)
                py(i) = inArray(i).Y
            Next
            Array.Sort(px)
            Array.Sort(py)
            ptMin.X = px(0)
            ptMin.Y = py(0)
            ptMax.X = px(UBound(px))
            ptMax.Y = py(UBound(py))
            ptMinMax(0) = ptMin
            ptMinMax(1) = ptMax
        Else
            ptMinMax = Nothing
        End If
        Return ptMinMax
    End Function

    Public Function retPointsSortedY(ByVal inArray() As Point) As Array
        inArray = UniquePoints(inArray)
        Dim yl As New SortedList
        Dim i As Int32
        Dim pt() As Point = Nothing
        If dimVar(inArray) Then
            For i = 0 To UBound(inArray)
                Try
                    If Not yl.ContainsKey(inArray(i).Y) Then
                        yl.Add(inArray(i).Y, inArray(i))
                    End If
                Catch ex As Exception
                End Try
            Next
            i = 0
            Dim pEnum As IDictionaryEnumerator = yl.GetEnumerator()
            While pEnum.MoveNext()
                ReDim Preserve pt(i)
                pt(i) = pEnum.Value
                i = i + 1
            End While
        End If
        Return pt
    End Function

#End Region


End Class
