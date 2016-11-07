Imports System.Drawing
Imports System.ComponentModel

Public Class SolidRoundDrawing
    Inherits ElementDrawing

    Public Overrides ReadOnly Property Type As String
        Get
            Type = "SolidDrawing"
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
                UpdateRectangle()
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
                UpdateRectangle()
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
                UpdateRectangle()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Height of the upper and lower rectangles
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
                UpdateRectangle()
            End If
        End Set
    End Property


    Private _rectangle As Rectangle
    Public Property Rectangle() As Rectangle
        Get
            Return _rectangle
        End Get
        Set(ByVal value As Rectangle)
            _rectangle = value
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


    Public Sub New(ByVal od As Single, ByVal len As Single, ByVal convFactor As Integer)

        PenStyle = Pens.Black
        BrushWidth = 5

        Try

            PointXUpper = 0
            PointYUpper = 0
            Width = CInt(len * convFactor)
            Height = CInt(od * convFactor)

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source)
        End Try

    End Sub

    'for downcasting from ElementDrawing
    Public Sub New(ByVal ele As ElementDrawing)

    End Sub

    ''' <summary>
    ''' Resizes the rectangle any time one of the parameters change
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateRectangle()

        Try
            If Not PointXUpper = Nothing Or PointYUpper = Nothing Or Width = Nothing Or Height = Nothing Then

                Rectangle = New Rectangle(PointXUpper, PointYUpper, Width, Height)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source)
        End Try

    End Sub

    Public Overrides Sub Resize(ByVal resizeFactor As Single)

        PointXUpper *= resizeFactor
        PointYUpper *= resizeFactor
        Width *= resizeFactor
        Height *= resizeFactor

    End Sub

    Public Overrides Sub Move(ByVal moveX As Single, ByVal moveY As Single)

        PointXUpper += moveX
        PointYUpper += moveY

    End Sub

    Public Overrides Sub Draw(ByVal g As Graphics)
        Dim htchSt As Drawing2D.HatchBrush
        htchSt = New Drawing2D.HatchBrush(Drawing2D.HatchStyle.BackwardDiagonal, Color.White, Color.Black)

        g.DrawRectangle(PenStyle, Rectangle)
        g.FillRectangle(htchSt, Rectangle)

    End Sub
End Class
