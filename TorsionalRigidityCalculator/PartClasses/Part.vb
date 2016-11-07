Public Class Part

    Private _partNumber As String
    Public Property PartNumber() As String
        Get
            Return _partNumber
        End Get
        Set(ByVal value As String)
            _partNumber = value
        End Set
    End Property

    Private _numberOfSections As Integer
    Public Property NumberOfSections() As Integer
        Get
            Return _numberOfSections
        End Get
        Set(ByVal value As Integer)
            _numberOfSections = value
        End Set
    End Property

    Private _customer As String
    Public Property Customer() As String
        Get
            Return _customer
        End Get
        Set(ByVal value As String)
            _customer = value
        End Set
    End Property

End Class
