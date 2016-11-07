Public Class Sections

    Private _sectionList As List(Of Section)
    Public Property SectionList() As List(Of Section)
        Get
            Return _sectionList
        End Get
        Set(ByVal value As List(Of Section))
            _sectionList = value
        End Set
    End Property

    Public Sub New()

        SectionList = New List(Of Section)

    End Sub

End Class
