Imports System.ComponentModel
Public Class Section

    Private _modulusOfRigidity As Single
    <CategoryAttribute("User Defined"), _
   DescriptionAttribute("Material property for the section in Pounds/in^2")>
    Public Property ModulusOfRigidity() As Single
        Get
            Return _modulusOfRigidity
        End Get
        Set(ByVal value As Single)
            _modulusOfRigidity = value
        End Set
    End Property
    Public Sub New()
        ModulusOfRigidity = 11500000
    End Sub

    Public Function CalculatedRigidity() As Single

        Throw New NotImplementedException

        Return CalculatedRigidity

    End Function
End Class
