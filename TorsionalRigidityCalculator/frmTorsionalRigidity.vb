Public Class frmTorsionalRigidity
    Dim sections As New Sections
    Dim shapes As New List(Of Shape)

    Private Sub btnAddTube_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTube.Click

        Dim tube As New TubeRoundShape
        shapes.Add(tube)
        RefreshSectionsList(tube.ToString)

    End Sub

    Private Sub btnAddSolid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSolid.Click

        Dim bar As New SolidRoundShape
        shapes.Add(bar)
        RefreshSectionsList(bar.ToString)

    End Sub

    Private Sub RefreshSectionsList(ByVal displayValue As String)

        lstbxSections.DataSource = Nothing
        lstbxSections.DataSource = shapes
        lstbxSections.DisplayMember = shapes.ToString
        UpdatePropertyGrid()

    End Sub

    Private Sub frmTorsionalRigidity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdatePropertyGrid()
    End Sub

    Private Sub lstbxSections_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstbxSections.SelectedIndexChanged
        UpdatePropertyGrid()
        lblCurrentIndex.Text = lstbxSections.SelectedIndex
    End Sub

    Private Sub pGridSections_PropertyValueChanged(ByVal s As System.Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pGridSections.PropertyValueChanged
        UpdatePropertyGrid()
    End Sub
    Private Sub UpdatePropertyGrid()
        pGridSections.SelectedObject = lstbxSections.SelectedItem
    End Sub

    Private Sub btnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
        Dim obj As Shape
        Dim index As Integer
        index = lstbxSections.SelectedIndex
        obj = shapes.Item(index)
        shapes.RemoveAt(lstbxSections.SelectedIndex)
        'loop through the list to add the items in the order you want...
    End Sub
End Class