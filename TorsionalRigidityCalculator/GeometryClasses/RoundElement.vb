Imports System.ComponentModel
Imports System.Drawing

Public MustInherit Class RoundElement
    Inherits ElementBase

    Private _outsideDiameter As Single
    <CategoryAttribute("User Defined"), _
   DescriptionAttribute("Outside diameter of the section")>
    Public Property OutsideDiameter() As Single

        Get
            Return _outsideDiameter
        End Get

        Set(ByVal value As Single)
            _outsideDiameter = value
            OutsideRadius = OutsideDiameter / 2
            RaiseEvent OutsideDiameterChanged(Me, New System.EventArgs)
        End Set

    End Property

    Private _outsideRadius As Single
    <CategoryAttribute("Calculated Values"), _
DescriptionAttribute("Calculated from Outside Diameter")>
    Public Property OutsideRadius() As Single
        Get
            Return _outsideRadius
        End Get
        Private Set(ByVal value As Single)
            _outsideRadius = value
        End Set
    End Property

    Private _length As Single
    <CategoryAttribute("User Defined"), _
DescriptionAttribute("Length of the section")>
    Public Property Length() As Single
        Get
            Return _length
        End Get

        Set(ByVal value As Single)
            _length = value
            RaiseEvent LengthChanged(Me, New System.EventArgs)
        End Set
    End Property

    Public Event OutsideDiameterChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event LengthChanged(ByVal sender As Object, ByVal e As EventArgs)

    Public MustOverride Property Drawing() As ElementDrawing
    Public MustOverride Property ModulusOfRigidity() As Single
    Public MustOverride Function Area() As Single
    Public MustOverride Function Volume() As Single
    Public MustOverride Function TorsionalRigidity() As Single

    ''' <summary>
    ''' Recreates the drawing based on the current element dimensions
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub RedrawAll()


End Class
