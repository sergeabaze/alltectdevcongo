using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace AllTech.FrameWork.Controls
{

    /// <summary>
    /// OptionsPanel is a panel control that can be drag within its parent control's boundries
    /// </summary>
    public class OptionsPanel : ItemsControl, INotifyPropertyChanged
    {
        ScrollViewer _scrollContent;       
        public OptionsPanel()
        {
            base.DefaultStyleKey = typeof(OptionsPanel);
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Right;

            Canvas.SetZIndex(this, 100);

            _winLocator = new WinLocator();
            this.Loaded += OnSampleOptionsPanelLoaded;            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            // default to top-right Alignment for Stretch Alignments
            if (this.HorizontalAlignment == HorizontalAlignment.Stretch)
                this.HorizontalAlignment = HorizontalAlignment.Right;
            if (this.VerticalAlignment == VerticalAlignment.Stretch)
                this.VerticalAlignment = VerticalAlignment.Top;

            if (this.Margin.Left == this.Margin.Right) _winLocator.MinX = this.Margin.Left;
            if (this.Margin.Top == this.Margin.Bottom) _winLocator.MinY = this.Margin.Top;
            
            var rootElement = VisualTreeHelper.GetChild(this, 0) as Grid;
            if (rootElement != null)
            {
                var winHandle = rootElement.FindName("optPanel") as Border;
                var btnState = rootElement.FindName("btnState") as ToggleButton;
                _scrollContent = rootElement.FindName("scrollContent") as ScrollViewer;

                if (winHandle != null)
                {
                    winHandle.MouseMove += OnWinHandleMouseMove;
                    winHandle.MouseLeftButtonDown += OnWinHandleMouseLeftButtonDown;
                    winHandle.MouseLeftButtonUp += OnWinHandleMouseLeftButtonUp;
                }

                if (btnState != null) btnState.Click += BtnStateClick;
            }
        }

        private void OnSampleOptionsPanelLoaded(object sender, RoutedEventArgs e)
        {
           
        }
       
        #region Fields
        bool _dragOn;
        bool _hasExpandedSize;
        private Point _borderPosition;
        private Point _currentPosition;
        private Size _expandedSize;
        private readonly WinLocator _winLocator;
        #endregion

        #region Methods
        /// <summary>
        /// Get position of this control relative to its HorizontalAlignment and VerticalAlignment
        /// </summary>
        /// <returns></returns>
        private Point GetRelativePosition()
        {
            Point postion = new Point();
            postion.X = this.HorizontalAlignment == HorizontalAlignment.Left ? this.Margin.Left : this.Margin.Right;
            postion.Y = this.VerticalAlignment == VerticalAlignment.Top ? this.Margin.Top : this.Margin.Bottom;
            return postion;
        }
        #endregion
        
        #region Event Handlers

        private void BtnStateClick(object sender, RoutedEventArgs e)
        {
            if (!_hasExpandedSize)
            {
                _hasExpandedSize = true;
                _expandedSize = new Size {Width = this.ActualWidth, Height = this.ActualHeight};
            }

            var btnState = (ToggleButton)sender;
            if (btnState.IsChecked != null)
            {
                _scrollContent.Visibility = btnState.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                btnState.Content = btnState.IsChecked.Value ? "-" : "+";
            }

            FrameworkElement parent = (FrameworkElement)this.Parent;
            if (btnState.IsChecked.Value)
            {
                _winLocator.MaxX = parent.ActualWidth - _expandedSize.Width - _winLocator.MinX;
                _winLocator.MaxY = parent.ActualHeight - _expandedSize.Height - _winLocator.MinY;
            }
            else
            {
                _winLocator.MaxX = parent.ActualWidth - this.ActualWidth - _winLocator.MinX;
                _winLocator.MaxY = parent.ActualHeight - this.ActualHeight - _winLocator.MinY;
            }
            _winLocator.FromPoint(GetRelativePosition());
            _winLocator.Validate();
            
            this.Margin = _winLocator.ToMargin(this.HorizontalAlignment, this.VerticalAlignment);
            
        }
        private void OnWinHandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var c = sender as FrameworkElement;
            _dragOn = true;
            this.Opacity *= 0.5;
            _borderPosition = e.GetPosition(sender as Border);
            if (c != null) c.CaptureMouse();

            _winLocator.MaxX = ((FrameworkElement)this.Parent).ActualWidth - this.ActualWidth - _winLocator.MinX;
            _winLocator.MaxY = ((FrameworkElement)this.Parent).ActualHeight - this.ActualHeight - _winLocator.MinY;
        }

        private void OnWinHandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_dragOn)
            {
                var c = sender as FrameworkElement;
                this.Opacity = 1;
                c.ReleaseMouseCapture();
                _dragOn = false;
            }
        }

        private void OnWinHandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragOn)
            {
                _currentPosition = e.GetPosition(sender as Border);
                Point newPosition = new Point();
    
                if (this.HorizontalAlignment == HorizontalAlignment.Left)
                    newPosition.X = this.Margin.Left + _currentPosition.X - _borderPosition.X;
                else
                    newPosition.X = this.Margin.Right - _currentPosition.X + _borderPosition.X;

                if (this.VerticalAlignment == VerticalAlignment.Top)
                    newPosition.Y = this.Margin.Top + _currentPosition.Y - _borderPosition.Y;
                else
                    newPosition.Y = this.Margin.Bottom - _currentPosition.Y + _borderPosition.Y;
                
                _winLocator.FromPoint(newPosition);
                _winLocator.Validate();
              
                this.Margin = _winLocator.ToMargin(this.HorizontalAlignment, this.VerticalAlignment);
            }
        }
        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
         "HeaderText", typeof(string), typeof(OptionsPanel), new PropertyMetadata(new PropertyChangedCallback(OnHeaderTextChanged)));

        private static void OnHeaderTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            OptionsPanel owner = (OptionsPanel)obj;
            owner.OnPropertyChanged("HeaderText");
        }
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        #endregion
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
    //public class OptionsPanel2 : ItemsControl, INotifyPropertyChanged
    //{
    //    ScrollViewer _scrollContent;
    //    public OptionsPanel2()
    //    {
    //        base.DefaultStyleKey = typeof(OptionsPanel);
    //        this.VerticalAlignment = VerticalAlignment.Top;
    //        this.HorizontalAlignment = HorizontalAlignment.Right;

    //        Canvas.SetZIndex(this, 100);

    //        _winLocator = new WinLocator();
    //        this.Loaded += OnSampleOptionsPanelLoaded;            
    //    }
    //    public override void OnApplyTemplate()
    //    {
    //        base.OnApplyTemplate();
            
    //        // default to top-right Alignment for Center or Stretch Alignments
    //        if (this.HorizontalAlignment == HorizontalAlignment.Center || 
    //            this.HorizontalAlignment == HorizontalAlignment.Stretch)
    //            this.HorizontalAlignment = HorizontalAlignment.Right;
    //        if (this.VerticalAlignment == VerticalAlignment.Center ||
    //            this.VerticalAlignment == VerticalAlignment.Stretch)
    //            this.VerticalAlignment = VerticalAlignment.Top;

    //        if (this.Margin.Left == this.Margin.Right) _winLocator.MinX = this.Margin.Left;
    //        if (this.Margin.Top == this.Margin.Bottom) _winLocator.MinY = this.Margin.Top;
            
    //        var rootElement = VisualTreeHelper.GetChild(this, 0) as Border;
    //        if (rootElement != null)
    //        {
    //            var winHandle = rootElement.FindName("optPanel") as Border;
    //            var btnState = rootElement.FindName("btnState") as ToggleButton;
    //            _scrollContent = rootElement.FindName("scrollContent") as ScrollViewer;

    //            if (winHandle != null)
    //            {
    //                winHandle.MouseMove += OnWinHandleMouseMove;
    //                winHandle.MouseLeftButtonDown += OnWinHandleMouseLeftButtonDown;
    //                winHandle.MouseLeftButtonUp += OnWinHandleMouseLeftButtonUp;
    //            }

    //            if (btnState != null) btnState.Click += BtnStateClick;
    //        }
    //    }

    //    private void OnSampleOptionsPanelLoaded(object sender, RoutedEventArgs e)
    //    {
           
    //    }
       
    //    #region Fields
    //    bool _dragOn = false;
    //    bool _hasExpandedSize = false;
    //    private Point _borderPosition;
    //    private Point _currentPosition;
    //    private Size _expandedSize;
    //    private readonly WinLocator _winLocator;
    //    #endregion

    //    #region Methods
    //    /// <summary>
    //    /// Get position of this control relative to its HorizontalAlignment and VerticalAlignment
    //    /// </summary>
    //    /// <returns></returns>
    //    private Point GetRelativePosition()
    //    {
    //        Point postion = new Point();
    //        postion.X = this.HorizontalAlignment == HorizontalAlignment.Left ? this.Margin.Left : this.Margin.Right;
    //        postion.Y = this.VerticalAlignment == VerticalAlignment.Top ? this.Margin.Top : this.Margin.Bottom;
    //        return postion;
    //    }
    //    #endregion
        
    //    #region Event Handlers

    //    private void BtnStateClick(object sender, RoutedEventArgs e)
    //    {
    //        if (!_hasExpandedSize)
    //        {
    //            _hasExpandedSize = true;
    //            _expandedSize = new Size {Width = this.ActualWidth, Height = this.ActualHeight};
    //        }

    //        var btnState = (ToggleButton)sender;
    //        _scrollContent.Visibility = btnState.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
    //        btnState.Content = btnState.IsChecked.Value ? "-" : "+";

    //        FrameworkElement parent = (FrameworkElement)this.Parent;
    //        if (btnState.IsChecked.Value)
    //        {
    //            _winLocator.MaxX = parent.ActualWidth - _expandedSize.Width - _winLocator.MinX;
    //            _winLocator.MaxY = parent.ActualHeight - _expandedSize.Height - _winLocator.MinY;
    //        }
    //        else
    //        {
    //            _winLocator.MaxX = parent.ActualWidth - this.ActualWidth - _winLocator.MinX;
    //            _winLocator.MaxY = parent.ActualHeight - this.ActualHeight - _winLocator.MinY;
    //        }
    //        _winLocator.FromPoint(GetRelativePosition());
    //        _winLocator.Validate();
            
    //        this.Margin = _winLocator.ToMargin(this.HorizontalAlignment, this.VerticalAlignment);
            
    //    }
    //    private void OnWinHandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    //    {
    //        var c = sender as FrameworkElement;
    //        _dragOn = true;
    //        this.Opacity *= 0.5;
    //        _borderPosition = e.GetPosition(sender as Border);
    //        c.CaptureMouse();

    //        _winLocator.MaxX = ((FrameworkElement)this.Parent).ActualWidth - this.ActualWidth - _winLocator.MinX;
    //        _winLocator.MaxY = ((FrameworkElement)this.Parent).ActualHeight - this.ActualHeight - _winLocator.MinY;
    //    }

    //    private void OnWinHandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    //    {
    //        if (_dragOn)
    //        {
    //            var c = sender as FrameworkElement;
    //            this.Opacity = 1;
    //            c.ReleaseMouseCapture();
    //            _dragOn = false;
    //        }
    //    }

    //    private void OnWinHandleMouseMove(object sender, MouseEventArgs e)
    //    {
    //        if (_dragOn)
    //        {
    //            _currentPosition = e.GetPosition(sender as Border);
    //            Point newPosition = new Point();
    
    //            if (this.HorizontalAlignment == HorizontalAlignment.Left)
    //                newPosition.X = this.Margin.Left + _currentPosition.X - _borderPosition.X;
    //            else
    //                newPosition.X = this.Margin.Right - _currentPosition.X + _borderPosition.X;

    //            if (this.VerticalAlignment == VerticalAlignment.Top)
    //                newPosition.Y = this.Margin.Top + _currentPosition.Y - _borderPosition.Y;
    //            else
    //                newPosition.Y = this.Margin.Bottom - _currentPosition.Y + _borderPosition.Y;
                
    //            _winLocator.FromPoint(newPosition);
    //            _winLocator.Validate();
              
    //            this.Margin = _winLocator.ToMargin(this.HorizontalAlignment, this.VerticalAlignment);
    //        }
    //    }
    //    #endregion

    //    #region Dependency Properties
    //    public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
    //     "HeaderText", typeof(string), typeof(OptionsPanel), new PropertyMetadata(new PropertyChangedCallback(OnHeaderTextChanged)));

    //    private static void OnHeaderTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    //    {
    //        OptionsPanel owner = (OptionsPanel)obj;
    //        owner.OnPropertyChanged("HeaderText");
    //    }
    //    public string HeaderText
    //    {
    //        get { return (string)GetValue(HeaderTextProperty); }
    //        set { SetValue(HeaderTextProperty, value); }
    //    }

    //    #endregion
    //    #region INotifyPropertyChanged Members

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected virtual void OnPropertyChanged(string name)
    //    {
    //        if (this.PropertyChanged != null)
    //            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
    //    }

    //    #endregion
    //}
    public class WinLocator
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double MinX { get; set; }
        public double MinY { get; set; }

        public double MaxX { get; set; }
        public double MaxY { get; set; }

        public void Validate()
        {
            if (X < MinX) X = MinX;
            if (Y < MinY) Y = MinY;
            if (X > MaxX) X = MaxX;
            if (Y > MaxY) Y = MaxY;
        }
        public Thickness ToMargin(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            Thickness newMargin = new Thickness(MinX, MinY, MinX, MinY);

            if (horizontalAlignment == HorizontalAlignment.Left)
                newMargin.Left = X;
            else
                newMargin.Right = X;

            if (verticalAlignment == VerticalAlignment.Top)
                newMargin.Top = Y;
            else
                newMargin.Bottom = Y;

            return newMargin;
        }
        public void FromPoint(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }
        public new string ToString()
        {
            return string.Format("X: {0} ({1}-{2}), Y: {3} ({4}-{5})", X, MinX, MaxX, Y, MinY, MaxY);
        }
    }
        
}
