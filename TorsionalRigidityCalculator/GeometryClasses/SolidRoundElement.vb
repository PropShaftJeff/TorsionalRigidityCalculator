Imports System.ComponentModel

Public Class SolidRoundElement
    Inherits RoundElement

    Public Overrides ReadOnly Property Type() As String
        Get
            Return "SolidRound"
        End Get
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

        Description = "Solid Round Section"
        ModulusOfRigidity = String.Format("{0:n0}", 11500000)
        OutsideDiameter = 3
        Length = 3

        Drawing = New SolidRoundDrawing(OutsideDiameter, Length, INCH_TO_PIXEL)

    End Sub

    'for downcasting from Element objects
    Public Sub New(ByVal base As ElementBase)

    End Sub

    Public Overrides Function TorsionalRigidity() As Single

        TorsionalRigidity = ((Math.PI * OutsideDiameter ^ 4 * ModulusOfRigidity) / (32 * Length)) / 12

    End Function

    Public Overrides Function ToString() As String
        Return Type
    End Function

    Public Overrides Function Area() As Single

        Area = Math.PI * (OutsideDiameter ^ 2) / 4
        Return Area

    End Function

    Public Overrides Function Volume() As Single

        Volume = Area() * Length
        Return Volume

    End Function

    ''' <summary>
    ''' Recreates the drawing based on the current element dimensions
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub RedrawAll()
        Drawing = Nothing
        Drawing = New SolidRoundDrawing(OutsideDiameter, Length, INCH_TO_PIXEL)
    End Sub

End Class
