Imports System.Drawing
Imports System.ComponentModel

Public Class HollowRoundDrawing
    Inherits ElementDrawing

    Public Overrides ReadOnly Property Type As String
        Get
            Type = "HollowDrawing"
        End Get
    End Property

    Private Property _penStyle As System.Drawing.Pen
    <Browsable(False)>
    Public Overrides Property PenStyle As System.Drawing.Pen
        Get
            Return _penStyle
        End Get
        Set(ByVal value As System.Drawing.Pen)
            _penStyle = value
        End Set
    End Property

    ''' <summary>
    ''' X coordinate of upper left point of uppper rectangle
    ''' </summary>
    ''' <remarks></remarks>
    Private _pointXUpper As Single

    Public Property PointXUpper() As Single
        Get
            Return _pointXUpper
        End Get
        Set(ByVal value As Single)
            If Not _pointXUpper = value Then
                _pointXUpper = value
                PointXLower = PointXUpper
            End If
        End Set
    End Property


    ''' <summary>
    ''' Y coordinate of upper left point of upper rectangle
    ''' </summary>
    ''' <remarks></remarks>
    Private _pointYUpper As Single

    Public Property PointYUpper() As Single
        Get
            Return _pointYUpper
        End Get
        Set(ByVal value As Single)
            If Not _pointYUpper = value Then
                _pointYUpper = value
                PointYLower = PointYUpper + Height - OuterHeight
            End If
        End Set
    End Property

    ''' <summary>
    ''' X coordinate of upper left point of lower rectangle
    ''' </summary>
    ''' <remarks></remarks>
    Private _pointXLower As Single

    Public Property PointXLower() As Single
        Get
            Return _pointXLower
        End Get
        Private Set(ByVal value As Single)
            If Not _pointXLower = value Then
                _pointXLower = value
            End If
        End Set
    End Property


    ''' <summary>
    ''' Y coordinate of upper left point of lower rectangle
    ''' </summary>
    ''' <remarks></remarks>
    Private _pointYLower As Single

    Public Property PointYLower() As Single
        Get
            Return _pointYLower
        End Get
        Private Set(ByVal value As Single)
            If Not _pointYLower = value Then
                _pointYLower = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Width of the upper and lower rectangles
    ''' </summary>
    ''' <remarks></remarks>
    Private _width As Single
    Public Overrides Property Width() As Single
        Get
            Return _width
        End Get
        Set(ByVal value As Single)
            If Not _width = value Then
                _width = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Combined height of two outer and the inner rectangles
    ''' </summary>
    ''' <remarks></remarks>
    Private _height As Single
    Public Overrides Property Height() As Single
        Get
            Return _height
        End Get
        Set(ByVal value As Single)
            If Not _height = value Then
                _height = value
            End If
        End Set
    End Property

    'Height of the upper and lower rectangles, ie the wall thickness of the tube
    Private _outerHeight As Single
    Public Property OuterHeight() As Single
        Get
            Return _outerHeight
        End Get
        Set(ByVal value As Single)
            _outerHeight = value
            RaiseEvent OuterHeightChanged(Me, New System.EventArgs)
        End Set
    End Property

    Private _upperRectangle As Rectangle
    Public Property UpperRectangle() As Rectangle
        Get
            Return _upperRectangle
        End Get
        Set(ByVal value As Rectangle)
            _upperRectangle = value
        End Set
    End Property

    Private _lowerRectangle As Rectangle
    Public Property LowerRectangle() As Rectangle
        Get
            Return _lowerRectangle
        End Get
        Set(ByVal value As Rectangle)
            _lowerRectangle = value
        End Set
    End Property

    Private _centerRectangle As Rectangle
    Public Property CenterRectangle() As Rectangle
        Get
            Return _centerRectangle
        End Get
        Set(ByVal value As Rectangle)
            _centerRectangle = value
        End Set
    End Property

    ''' <summary>
    ''' Gets and sets the brush width of the shape
    ''' </summary>
    ''' <remarks></remarks>
    Private _brushWidth As Integer
    Public Overrides Property BrushWidth As Integer
        Get
            Return _brushWidth
        End Get
        Set(ByVal value As Integer)
            _brushWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Create a new instance of a hollow round drawing
    ''' </summary>
    ''' <param name="od">outer diameter of the drawing</param>
    ''' <param name="len">length of the drawing</param>
    ''' <param name="wall">wall thickness of the drawing</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal od As Single, ByVal len As Single, ByVal wall As Single, ByVal convFactor As Integer)

        PenStyle = Pens.Black
        BrushWidth = 5

        Try
            'the order is important here
            OuterHeight = CInt(wall * convFactor)
            Height = od * convFactor

            PointXUpper = 0
            PointYUpper = 0
            Width = CInt(len * convFactor)

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source)
        End Try

    End Sub
    'for downcasting from ElementDrawing
    Public Sub New(ByVal ele As ElementDrawing)

    End Sub

    ''' <summary>
    ''' Resizes the rectangles any time the one of the parameter values changes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateRectangles()

        Try
            If Not PointXUpper = Nothing Or PointYUpper = Nothing Or Width = Nothing Or OuterHeight = Nothing Or _
                PointXLower = Nothing Or PointYLower = Nothing Then

                UpperRectangle = New Rectangle(PointXUpper, PointYUpper, Width, OuterHeight)
                LowerRectangle = New Rectangle(PointXLower, PointYLower, Width, OuterHeight)
                CenterRectangle = New Rectangle(PointXUpper, PointYUpper + OuterHeight, Width, PointYLower - (PointYUpper + OuterHeight))

                'Height = UpperRectangle.Height + LowerRectangle.Height + CenterRectangle.Height

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message, ex.Source)

        End Try

    End Sub

    Public Overrides Sub Resize(ByVal resizeFactor As Single)

        OuterHeight *= resizeFactor
        Height *= resizeFactor
        'PointXUpper *= resizeFactor
        'PointYUpper *= resizeFactor
        Width *= resizeFactor

    End Sub

    Public Overrides Sub Move(ByVal moveX As Single, ByVal moveY As Single)

        PointXUpper += moveX
        PointYUpper += moveY
        'PointXLower += PointXLower
        'PointYLower += PointYLower

    End Sub

    Public Event OuterHeightChanged(ByVal sender As Object, ByVal e As EventArgs)
    Public Event InnerHeightChanged(ByVal sender As Object, ByVal e As EventArgs)

    Public Overrides Sub Draw(ByVal g As System.Drawing.Graphics)

        Dim htchSt As Drawing2D.HatchBrush
        htchSt = New Drawing2D.HatchBrush(Drawing2D.HatchStyle.BackwardDiagonal, Color.White, Color.Black)

        UpdateRectangles()

        g.DrawRectangle(PenStyle, UpperRectangle)
        g.DrawRectangle(PenStyle, CenterRectangle)
        g.DrawRectangle(PenStyle, LowerRectangle)

        'fill with a crosshatch pattern
        g.FillRectangle(htchSt, LowerRectangle)
        g.FillRectangle(htchSt, UpperRectangle)

    End Sub
End Class
