Imports System.Drawing

Public Class frmTorsionalRigidity

    Dim eleGlobal As ElementBase
    Dim bdrWdth As Integer = 20
    Dim usableWidth As Integer
    Dim usableHeight As Integer


    Private Sub btnAddTube_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTube.Click    

        Dim tube As New HollowRoundElement

        eleGlobal.Elements.Add(tube)

        RefreshSectionsList(tube.ToString & "-" & tube.Description)

        lstbxSections.SelectedItem = tube

        RefreshDrawing()

    End Sub

    Private Sub btnAddSolid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSolid.Click

        Dim bar As New SolidRoundElement

        eleGlobal.Elements.Add(bar)

        RefreshSectionsList(bar.ToString & "-" & bar.Description)

        lstbxSections.SelectedItem = bar

        RefreshDrawing()

    End Sub

    Private Sub RefreshSectionsList(ByVal displayValue As String)

        UpdateListBox(-1)
        UpdatePropertyGrid()

    End Sub
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        usableWidth = pnlSketch.Width - 2 * bdrWdth
        usableHeight = pnlSketch.Height - 2 * bdrWdth
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmTorsionalRigidity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        eleGlobal = New ElementBase

    End Sub

    Private Sub lstbxSections_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstbxSections.SelectedIndexChanged
        UpdatePropertyGrid()
    End Sub

    Private Sub pGridSections_PropertyValueChanged(ByVal s As System.Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pGridSections.PropertyValueChanged

        Dim sel As Integer
        sel = lstbxSections.SelectedIndex

        UpdatePropertyGrid()
        UpdateListBox(sel)

        RefreshDrawing()

    End Sub

    Private Sub btnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click

        MoveListboxObject(sender)
        RefreshDrawing()


    End Sub

    Private Sub btnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click

        MoveListboxObject(sender)
        RefreshDrawing()

    End Sub

    'COMMENT OUT WHEN DEBUGGING
    Private Sub GetMousePosition(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove, btnAddSolid.MouseMove, _
        btnAddTube.MouseMove, btnCalculate.MouseMove, btnMoveDown.MouseMove, btnMoveUp.MouseMove, lstbxSections.MouseMove, txtRigidity.MouseMove, lblUnits.MouseMove, _
        pnlSketch.MouseMove

        lbltStatus.Text = String.Format("X: {0}  Y: {1} ", e.X, e.Y)

    End Sub

    'updates the property grid to display the current item added to the list
    Private Sub UpdatePropertyGrid()

        pGridSections.SelectedObject = lstbxSections.SelectedItem

    End Sub

    Private Sub UpdateListBox(ByVal selIndex As Integer)

        Try
            Dim selItm As Integer
            selItm = lstbxSections.SelectedIndex
            lstbxSections.DataSource = Nothing
            lstbxSections.DataSource = eleGlobal.Elements
            lstbxSections.DisplayMember = eleGlobal.Description
            If selIndex >= 0 Then
                lstbxSections.SelectedIndex = selIndex
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "frmTorsionalRigidity.UpdateListBox")
        End Try

    End Sub

    Public Sub MoveListboxObject(ByVal sender As System.Object)

        Dim obj As ElementBase
        Dim index As Integer
        obj = lstbxSections.SelectedItem
        index = eleGlobal.Elements.IndexOf(obj)

        If sender.Equals(btnMoveDown) Then

            If index = lstbxSections.Items.Count - 1 Then

                Exit Sub

            Else

                eleGlobal.Elements.Insert(index + 2, obj)
                eleGlobal.Elements.RemoveAt(index)

                UpdateListBox(index + 1)

            End If

        ElseIf sender.Equals(btnMoveUp) Then

            If index = 0 Then
                Exit Sub
            Else

                eleGlobal.Elements.Insert(index - 1, obj)
                eleGlobal.Elements.RemoveAt(index + 1)

                UpdateListBox(index - 1)

            End If

        End If

    End Sub

    Public Function OverallTorsionalRigidity() As Single

        Dim rigVals As New List(Of Single)
        Dim sumVals As Single

        For Each objEle In eleGlobal.Elements
            rigVals.Add(objEle.TorsionalRigidity)
        Next

        For Each value In rigVals
            sumVals += 1 / value
        Next

        OverallTorsionalRigidity = 1 / sumVals

    End Function

    Private Sub btnCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculate.Click

        txtRigidity.Text = String.Format("{0:n0}", OverallTorsionalRigidity())

    End Sub

    Private Sub lstbxSections_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstbxSections.KeyDown
        If e.KeyCode = Keys.Delete Then
            Dim loc As Integer
            loc = lstbxSections.SelectedIndex
            eleGlobal.Elements.Remove(lstbxSections.SelectedItem)
            If loc = 0 Then
                UpdateListBox(0)
            Else
                UpdateListBox(loc - 1)
            End If

            'redraw the paint area to remove old sketches
            pnlSketch.Refresh()
            eleGlobal.RefreshDrawings()

            'get overall size of all the drawings
            GetResizeValue()

            ResizeDrawings()
            MoveDrawings()

            Dim g As Graphics = Graphics.FromHwnd(Me.pnlSketch.Handle)
            DrawAllRectangles(g)

        End If
    End Sub
    ''' <summary>
    ''' Gets the value to resize the drawings based on the panel size and the drawing collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetResizeValue()

        eleGlobal.GetDrawingWidth()
        eleGlobal.GetDrawingHeight()
        eleGlobal.GetAspectRatio()

        'figure out how to resize based on aspect ratios of drawing canvas
        eleGlobal.ResizeFactor = eleGlobal.AspectRatio / (usableWidth / usableHeight)
       
    End Sub

    Private Sub frmTorsionalRigidity_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged

        'figure out how to resize based on aspect ratios of drawing canvas
        If Not eleGlobal Is Nothing Then
           
            usableWidth = pnlSketch.Width - 2 * bdrWdth
            usableHeight = pnlSketch.Height - 2 * bdrWdth

            eleGlobal.ResizeFactor = eleGlobal.AspectRatio / (usableWidth / usableHeight)
            
            ResizeDrawings()
            RefreshDrawing()

        End If

    End Sub

    Private Sub ResizeDrawings()

        'use resizeFactor global instead of calculation in If statement

        If eleGlobal.AspectRatio > (usableWidth / usableHeight) Then

            Dim widthRed As Single
            'widthRed = eleGlobal.OverallWidth / usableWidth
            If eleGlobal.OverallWidth > 0 Then
                widthRed = usableWidth / eleGlobal.OverallWidth
                'resize the global container
                eleGlobal.OverallWidth *= widthRed
                eleGlobal.OverallHeight *= widthRed
            Else
                Exit Sub
            End If

            For Each ele In eleGlobal.Elements
                ele.Drawing.Resize(widthRed)
            Next

        ElseIf eleGlobal.AspectRatio < (usableWidth / usableHeight) Then

            Dim heightRed As Single
            If eleGlobal.OverallHeight > 0 Then
                heightRed = usableHeight / eleGlobal.OverallHeight
                'resize the global container
                eleGlobal.OverallWidth *= heightRed
                eleGlobal.OverallHeight *= heightRed
            Else
                Exit Sub
            End If

            For Each ele In eleGlobal.Elements
                ele.Drawing.Resize(heightRed)
            Next

        Else
            'this is a repeat of the first else condition, if the aspect is the same,
            'let the width be the controlling factor for resizing.

            Dim widthRed As Single
            If usableWidth > 0 Then
                widthRed = eleGlobal.OverallWidth / usableWidth
                'resize the global container
                eleGlobal.OverallWidth *= widthRed
                eleGlobal.OverallHeight *= widthRed
            Else
                Exit Sub
            End If

            For Each ele In eleGlobal.Elements
                ele.Drawing.Resize(widthRed)
            Next

        End If

        'GetShapeSizes()

    End Sub

    ''' <summary>
    ''' Moves the collection of drawings into the proper place on the panel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MoveDrawings()
        Dim moveDist As Integer

        For i = 0 To eleGlobal.Elements.Count - 1
            If i = 0 Then

                Dim moveX As Integer
                Dim moveY As Integer

                moveY = pnlSketch.Height / 2 - eleGlobal.Elements.Item(i).Drawing.Height / 2
                moveX = pnlSketch.Width / 2 - eleGlobal.OverallWidth / 2

                eleGlobal.Elements.Item(i).Drawing.Move(moveX, moveY)

                moveDist = moveX

            Else

                Dim moveX As Integer
                Dim moveY As Integer

                moveY = pnlSketch.Height / 2 - eleGlobal.Elements.Item(i).Drawing.Height / 2
                moveX = moveDist + eleGlobal.Elements.Item(i - 1).Drawing.Width

                eleGlobal.Elements.Item(i).Drawing.Move(moveX, moveY)

                moveDist = moveX

            End If
        Next

    End Sub

    Public Sub DrawAllRectangles(ByVal g As Graphics)

        For Each ele In eleGlobal.Elements

            ele.Drawing.Draw(g)

        Next
    End Sub

    ''' <summary>
    ''' Refreshes the drawing when necessary to reflect changes to elements or panel size
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshDrawing()
        'redraw the paint area to remove old sketches
        pnlSketch.Refresh()
        eleGlobal.RefreshDrawings()

        'get overall size of all the drawings
        GetResizeValue()

        ResizeDrawings()
        MoveDrawings()

        Dim g As Graphics = Graphics.FromHwnd(Me.pnlSketch.Handle)
        DrawAllRectangles(g)
    End Sub

End Class