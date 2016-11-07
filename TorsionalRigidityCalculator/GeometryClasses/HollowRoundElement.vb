Imports System.ComponentModel
Public Class HollowRoundElement
    Inherits RoundElement

    Public Overrides ReadOnly Property Type() As String
        Get
            Return "Tube"
        End Get
    End Property

    Private _insideDiameter As Single
    <CategoryAttribute("Calculated Values"), _
    DescriptionAttribute("Calculated from Outside Diameter and Wall Thickness")>
    Public Property InsideDiameter() As Single
        Get
            Return _insideDiameter
        End Get
        Private Set(ByVal value As Single)
            _insideDiameter = value
        End Set
    End Property

    Private _wallThickness As Single
    <CategoryAttribute("User Defined"), _
   DescriptionAttribute("Wall thickness of this tube section")>
    Public Property WallThickness() As Single
        Get
            Return _wallThickness
        End Get
        Set(ByVal value As Single)
            _wallThickness = value
            InsideDiameter = OutsideDiameter - 2 * WallThickness
        End Set
    End Property

    ''' <summary>
    ''' The drawing object for this element
    ''' </summary>
    ''' <remarks></remarks>
    Private _drawing As ElementDrawing
    <Browsable(False)>
    Public Overrides Property Drawing() As ElementDrawing
        Get
            Return _drawing
        End Get
        Set(ByVal value As ElementDrawing)
            _drawing = value
        End Set
    End Property

    Private _modulusOfRigidity As Single
    <CategoryAttribute("User Defined"), _
   DescriptionAttribute("Material property for the section in Pounds/in^2")>
    Public Overrides Property ModulusOfRigidity() As Single
        Get
            Return _modulusOfRigidity
        End Get
        Set(ByVal value As Single)
            _modulusOfRigidity = value
        End Set
    End Property

    Public Sub New()

        'give some default values so the drawing object is not null
        Description = "Tube Section"
        ModulusOfRigidity = String.Format("{0:n0}", 11500000)
        OutsideDiameter = 3.0
        Length = 3.0
        WallThickness = 0.25

        Drawing = New HollowRoundDrawing(OutsideDiameter, Length, WallThickness, INCH_TO_PIXEL)

    End Sub
    'for downcasting from Element objects
    Public Sub New(ByVal base As ElementBase)

    End Sub

    ''' <summary>
    ''' Gets the area of a cross section of tubing
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Area() As Single

        Area = Math.PI * (OutsideDiameter ^ 2 - InsideDiameter ^ 2) / 4
        Return Area

    End Function

    ''' <summary>
    ''' Gets the volume of a length of this tubing
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Volume() As Single

        Volume = Area() * Length
        Return Volume

    End Function

    Public Overrides Function ToString() As String

        Return Type

    End Function

    Public Overrides Function TorsionalRigidity() As Single

        TorsionalRigidity = ((Math.PI * (OutsideDiameter ^ 4 - InsideDiameter ^ 4) * ModulusOfRigidity) / (32 * Length)) / 12

    End Function

    ''' <summary>
    ''' Recreates the drawing based on the current element dimensions
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub RedrawAll()

        Drawing = Nothing
        Drawing = New HollowRoundDrawing(OutsideDiameter, Length, WallThickness, INCH_TO_PIXEL)

    End Sub

    Private Sub UpdateInsideDiameter() Handles MyBase.OutsideDiameterChanged

        InsideDiameter = OutsideDiameter - 2 * WallThickness

    End Sub

End Class
