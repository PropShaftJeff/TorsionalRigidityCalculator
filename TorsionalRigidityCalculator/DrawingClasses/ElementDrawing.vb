Imports System.ComponentModel

Public MustInherit Class ElementDrawing

    Public MustOverride ReadOnly Property Type() As String

    ''' <summary>
    ''' Width in pixels of the brush used for the drawing pen
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Property BrushWidth() As Integer
    Public MustOverride Property PenStyle() As System.Drawing.Pen

    Public MustOverride Property Height() As Single
    Public MustOverride Property Width() As Single

    Public MustOverride Sub Resize(ByVal convFactor As Single)
    Public MustOverride Sub Move(ByVal moveX As Single, ByVal moveY As Single)
    Public MustOverride Sub Draw(ByVal g As Graphics)

End Class
