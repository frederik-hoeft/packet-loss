using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace PostOffice.Gui;

internal class Listing_Scrollable
{
    private const float ScrollViewWidthDelta = 25f;
    private Rect _container;
    private Rect _content;

    private Vector2 _scrollPosition = Vector2.zero;

    public Rect Container => _container;

    public Rect Content => _content;

    public void Begin(Rect canvas)
    {
        _container = canvas;
        _content = new Rect(0f, 0f, canvas.width - ScrollViewWidthDelta, 0f);
        Widgets.BeginScrollView(canvas, ref _scrollPosition, _content);
    }

    public void End()
    {
        Widgets.EndScrollView();
    }
}
