Imports System.ComponentModel

''' <summary>
''' Base class for working with different cross sectional elements
''' </summary>
''' <remarks></remarks>
Public Class ElementBase

    Public Const INCH_TO_PIXEL As Integer = 96

    'used for resizing the drawings
    Private _resizeFactor As Single
    <Browsable(False)>
    Public Property ResizeFactor() As Single
        Get
            Return _resizeFactor
        End Get
        Set(ByVal value As Single)
            _resizeFactor = value
        End Set
    End Property

    Private _overallWidth As Integer
    <Browsable(False)>
    Public Property OverallWidth() As Integer
        Get
            Return _overallWidth
        End Get
        Set(ByVal value As Integer)
            _overallWidth = value
            GetAspectRatio()
        End Set
    End Property

    Private _overallHeight As Integer
    <Browsable(False)>
    Public Property OverallHeight() As Integer
        Get
            Return _overallHeight
        End Get
        Set(ByVal value As Integer)
            _overallHeight = value
            GetAspectRatio()
        End Set
    End Property

    Private _description As String
    <CategoryAttribute("User Defined"), _
   DescriptionAttribute("Description of this section")>
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _aspectRatio As Single
    <Browsable(False)>
    Public Property AspectRatio() As Single
        Get
            Return _aspectRatio
        End Get
        Private Set(ByVal value As Single)
            _aspectRatio = value
        End Set
    End Property

    Private _elements As List(Of RoundElement)
    <Browsable(False)>
    Public Property Elements() As List(Of RoundElement)
        Get
            Return _elements
        End Get
        Set(ByVal value As List(Of RoundElement))
            _elements = value
        End Set
    End Property

    Public Overridable ReadOnly Property Type() As String

        Get
            Return "Elements"
        End Get

    End Property


    Public Sub New()

        OverallHeight = 0
        OverallWidth = 0
        AspectRatio = 0
        Elements = New List(Of RoundElement)

    End Sub
    ''' <summary>
    ''' Gets the width of all of the Retangles in the list
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetDrawingWidth()

        OverallWidth = 0 'reset width

        For Each ele In Elements
            OverallWidth += ele.Drawing.Width
        Next

    End Sub
    ''' <summary>
    ''' Gets the maximum height of the Rectangles in the list
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetDrawingHeight()

        OverallHeight = 0 'reset height
        Dim tempHt As Integer = 0

        For Each ele In Elements
            If ele.Drawing.Height > tempHt Then
                tempHt = ele.Drawing.Height
                OverallHeight = ele.Drawing.Height
            Else
                OverallHeight = tempHt
            End If
        Next

    End Sub
    ''' <summary>
    ''' Gets the aspect ratio of the outer bounds of the rectangles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetAspectRatio()

        If OverallHeight = 0 Or OverallWidth = Nothing Then
            Exit Sub
        Else
            AspectRatio = OverallWidth / OverallHeight
        End If

    End Sub

    Public Sub RefreshDrawings()
        For Each ele In Elements
            ele.RedrawAll()
        Next
    End Sub
End Class
