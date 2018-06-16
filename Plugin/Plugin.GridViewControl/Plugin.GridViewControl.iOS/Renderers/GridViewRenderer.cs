using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Plugin.GridViewControl.Common;
using Plugin.GridViewControl.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using System.Reflection;
using System.Diagnostics;

[assembly: ExportRenderer (typeof(GridView), typeof(GridViewRenderer))]
namespace Plugin.GridViewControl.iOS.Renderers
{
    #region GridViewRenderer

    /// <summary>
    /// Class GridViewRenderer.
    /// </summary>
    public class GridViewRenderer : ViewRenderer<GridView, GridCollectionView>, IGridViewProvider
    {
        #region Fields

        UIRefreshControl _pullToRefresh;

        /// <summary>
        /// Instance of navtive view.
        /// </summary>
        GridCollectionView _gridCollectionView;

        /// <summary>
        /// Starting index for scrolling events.
        /// </summary>
        int? _initialIndex;

        /// <summary>
        /// The _data source
        /// </summary>
        private GridDataSource _dataSource;


        NSString cellId, headerId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GridViewRenderer"/> class.
        /// </summary>l
        public GridViewRenderer()
        {
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets the data source.
        /// </summary>
        /// <value>The data source.</value>
        private GridDataSource DataSource
        {
            get
            {
                return _dataSource ??
                (_dataSource =
                        new GridDataSource(GetCell, RowsInSection, ItemSelected, NumberOfSections, GetViewForSupplementaryElement));
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<GridView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
            {
                return;
            }
            e.NewElement.GridViewProvider = this;

            _gridCollectionView = new GridCollectionView();
            _gridCollectionView.AllowsMultipleSelection = false;
            _gridCollectionView.BackgroundColor = Element.BackgroundColor.ToUIColor();
            _gridCollectionView.AddGestureRecognizer(new UILongPressGestureRecognizer(OnLongPress));


            _pullToRefresh = new UIRefreshControl();
            _pullToRefresh.Enabled = Element.IsPullToRefreshEnabled;
            _gridCollectionView.AddSubview(_pullToRefresh);
            _pullToRefresh.ValueChanged += _pullToRefresh_ValueChanged;
            _gridCollectionView.AlwaysBounceVertical = true;

            //var flowLayout = new UICollectionViewFlowLayout();

            //Unbox the collection view layout manager.
            //UICollectionViewFlowLayout flowLayout = (UICollectionViewFlowLayout)_gridCollectionView.CollectionViewLayout;


            //Remove any section or content insets.
            //_gridCollectionView.ContentInset = new UIEdgeInsets(0, 0, 0, 0);
            //flowLayout.SectionInset = new UIEdgeInsets(10, 0, 10, 0);
            //_gridCollectionView.SetCollectionViewLayout(flowLayout, false);

            //Remove event handling..
            Unbind(e.OldElement);

            //Add event handling.
            Bind(e.NewElement);

            //Set the data source.  
            _gridCollectionView.DataSource = (e.NewElement.ItemsSource != null) ? DataSource : null;
            _gridCollectionView.Delegate = new GridViewDelegate(ItemSelected, HandleOnScrolled);

            //If we are dealing with groups.
            if (e.NewElement.ItemsSource != null && 
                e.NewElement.ItemsSource.OfType<IEnumerable>().Any() && 
                e.NewElement.GroupHeaderTemplate != null)
            {
                //Unbox the layout.
                var flowLayout = ((UICollectionViewFlowLayout)_gridCollectionView.CollectionViewLayout);

                //This smells bad but I don't know of a better way to accomplish this.
                //I'm rendering the first header cell in order to establish the size.
                //Of course this means that all headers will be the same size and some
                //clipping could occur depdending on the data.
                var cell = Element.GroupHeaderTemplate.CreateContent() as ViewCell;
                cell.BindingContext = Element.ItemsSource.Cast<IEnumerable>().First();

                //Set the reference size accordingly.
                flowLayout.HeaderReferenceSize = new CGSize(Element.Width, cell.View.HeightRequest != -1 ? cell.View.HeightRequest : cell.RenderHeight);
            }

            //Scroll to first item.
            ScrollToInitialIndex();

            //Set the native control.
            SetNativeControl(_gridCollectionView);
        }

        private void OnLongPress(UILongPressGestureRecognizer gestureRecognizer)
        {
            if (gestureRecognizer.State != UIGestureRecognizerState.Ended)
                return;

            var point = gestureRecognizer.LocationInView(_gridCollectionView);

            var indexPath = _gridCollectionView.IndexPathForItemAtPoint(point);
            if (indexPath == null)
            {
                Debug.WriteLine("Couldn't find index path");
            }
            else
            {
                var cell = _gridCollectionView.CellForItem(indexPath);
                if (cell is GridViewCell bindableCell)
                    Element.RaiseOnItemHold(bindableCell.ViewCell.BindingContext);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _pullToRefresh_ValueChanged(object sender, EventArgs e)
        {
            //If the refresh command is not nothing.
            if (Element.RefreshCommand != null)
            {
                Element.IsRefreshing = true;
                Element.RefreshCommand.Execute(null);
            }
            else
            {
                //Stop refreshing.
                _pullToRefresh.EndRefreshing();
            }
        }


        /// <summary>
        /// Set the item size according to the min item width property.
        /// </summary>
        /// <param name="sender">System.Object repersenting the source of the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void ElementSizeChanged(object sender, EventArgs e)
        {
            //Unbox the grid view.
            var gridView = sender as GridView;

            //Work out how many items can fit.
            var canfit = (int)Math.Max(1, Math.Floor(gridView.Width / gridView.MinItemWidth));

            //Stretch the width to fill row.
            var actualWidth = gridView.Width / canfit;

            //Set the item size accordingly.
            _gridCollectionView.ItemSize = new CoreGraphics.CGSize(actualWidth, (float)gridView.RowHeight);
        }

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var gridView = sender as GridView;

            if (e.PropertyName == "ItemsSource")
            {
                if (gridView.ItemsSource != null)
                {
                    _gridCollectionView.DataSource = DataSource;
                    ReloadData();
                    ScrollToInitialIndex();
                }
            }
            //If the element IsRefreshing property is changing to false.
            else if (e.PropertyName == "IsRefreshing" && !Element.IsRefreshing)
            {
                //Stop refreshing.
                _pullToRefresh.EndRefreshing();
            }
            //If the element PullToRefresh property is changing.
            else if (e.PropertyName == "IsPullToRefreshEnabled")
            {
                //Indicate whether pull to refresh is enabled.
                _pullToRefresh.Enabled = Element.IsPullToRefreshEnabled;
            }
        }

        /// <summary>
        /// Unbinds the specified old element.
        /// </summary>
        /// <param name="oldElement">The old element.</param>
        private void Unbind(GridView oldElement)
        {
            if (oldElement != null)
            {
                oldElement.PropertyChanging -= ElementPropertyChanging;
                oldElement.PropertyChanged -= ElementPropertyChanged;
                oldElement.SizeChanged -= ElementSizeChanged;
                var itemsSource = oldElement.ItemsSource as INotifyCollectionChanged;
                if (itemsSource != null)
                {
                    itemsSource.CollectionChanged -= DataCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Binds the specified new element.
        /// </summary>
        /// <param name="newElement">The new element.</param>
        private void Bind(GridView newElement)
        {
            if (newElement != null)
            {
                newElement.PropertyChanging += ElementPropertyChanging;
                newElement.PropertyChanged += ElementPropertyChanged;
                newElement.SizeChanged += ElementSizeChanged;
                if (newElement.ItemsSource is INotifyCollectionChanged)
                {
                    (newElement.ItemsSource as INotifyCollectionChanged).CollectionChanged += DataCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Elements the property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemsSource")
            {
                var itemsSource = Element != null ? Element.ItemsSource as INotifyCollectionChanged : null;
                if (itemsSource != null)
                {
                    itemsSource.CollectionChanged -= DataCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Elements the property changing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangingEventArgs"/> instance containing the event data.</param>
        private void ElementPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == "ItemsSource")
            {
                var itemsSource = Element != null ? Element.ItemsSource as INotifyCollectionChanged : null;
                if (itemsSource != null)
                {
                    itemsSource.CollectionChanged += DataCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Datas the collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void DataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Control != null)
            {
                ReloadData();
            }
        }

        void HandleOnScrolled(CGPoint contentOffset)
        {
            foreach (GridViewCell nativeCell in _gridCollectionView.VisibleCells)
            {
                nativeCell.ViewCell.OnScroll(contentOffset.ToPoint(), new Xamarin.Forms.Point(nativeCell.Frame.X, nativeCell.Frame.Y));
            }
            Element.RaiseOnScroll(0, (float)contentOffset.Y);
        }

        void ScrollToInitialIndex()
        {
            if (_initialIndex.HasValue && _gridCollectionView != null && _gridCollectionView.DataSource != null)
            {
                ScrollToItemWithIndex(_initialIndex.Value, false);
                _initialIndex = null;
            }
        }

        /// <summary>
        /// Rowses the in section.
        /// </summary>
        /// <param name="collectionView">The collection view.</param>
        /// <param name="section">The section.</param>
        /// <returns>System.Int32.</returns>
        public int RowsInSection(UICollectionView collectionView, nint section)
        {

            int numberOfItems = 0;

            //Get the sections.
            var sections = Element.ItemsSource.OfType<IEnumerable>().ToList();

            //If there are any sections.
            if (sections.Any())
            {
                numberOfItems = sections[(int)section].Cast<object>().Count();
            }
            else
            {
                numberOfItems = ((ICollection)Element.ItemsSource).Count;
            }


            return numberOfItems;
        }

        /// <summary>
        /// Items the selected.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        public void ItemSelected(UICollectionView tableView, NSIndexPath indexPath)
        {
            //Get the sections.
            var sections = Element.ItemsSource.OfType<IEnumerable>().ToList();

            object item;

            //If there are any sections.
            if (sections.Any())
            {
                item = sections[(int)indexPath.Section].Cast<object>().ElementAt(indexPath.Row);
            }
            else
            {
                item = Element.ItemsSource.Cast<object>().ElementAt(indexPath.Row);
            }

            Element.InvokeItemSelectedEvent(this, item);
        }

        public int NumberOfSections(UICollectionView collectionView)
        {
            return Math.Max(1, Element.ItemsSource.OfType<IEnumerable>().Count());
        }

        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <returns>The cell.</returns>
        /// <param name="collectionView">Collection view.</param>
        /// <param name="indexPath">Index path.</param>
        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            cellId = cellId ?? new NSString(GridViewCell.Key);

            //Get the sections.
            var sections = Element.ItemsSource.OfType<IEnumerable>().ToList();

            object item;

            //If there are any sections.
            if (sections.Any())
            {
                item = sections[(int)indexPath.Section].Cast<object>().ElementAt(indexPath.Row);
            }
            else
            {
                item = Element.ItemsSource.Cast<object>().ElementAt(indexPath.Row);
            }

        

            var collectionCell = collectionView.DequeueReusableCell(cellId, indexPath) as GridViewCell;
            //collectionCell.SelectedBackgroundView = new UIView()
            //{
            //    BackgroundColor = UIColor.Red,
            //    Hidden = false,


            //};
            //collectionCell.SelectedBackgroundView.Layer.RemoveAllAnimations();

            //var a = NSBundle.MainBundle.LoadNib("statelist_item_background", null, null);
            //collectionCell.SelectedBackgroundView = ObjCRuntime.Runtime.GetNSObject<UIView>(a.ValueAt(0));
            //collectionCell.SelectedBackgroundView.Layer.RemoveAllAnimations();

            collectionCell.RecycleCell(item, Element.ItemTemplate, Element);
            return collectionCell;
        }

        public UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            headerId = headerId ?? new NSString(GridViewCell.HeaderKey);

            var headerCell = collectionView.DequeueReusableSupplementaryView(elementKind, headerId, indexPath) as GridViewCell;

            var item = Element.ItemsSource.Cast<object>().ElementAt(indexPath.Section);

            headerCell.RecycleCell(item, Element.GroupHeaderTemplate, Element);

            return headerCell;
        }

        /// <summary>
        /// Reloads the data.
        /// </summary>
        public void ReloadData()
        {
            if (_gridCollectionView != null)
            {
                InvokeOnMainThread(() =>
                {
                    //If we are dealing with groups.
                    if (Element.ItemsSource.OfType<IEnumerable>().Any() && 
                    Element.GroupHeaderTemplate != null)
                    {
                        //Unbox the layout.
                        var flowLayout = ((UICollectionViewFlowLayout)_gridCollectionView.CollectionViewLayout);

                        //This smells bad but I don't know of a better way to accomplish this.
                        //I'm rendering the first header cell in order to establish the size.
                        //Of course this means that all headers will be the same size and some
                        //clipping could occur depdending on the data.
                        var cell = Element.GroupHeaderTemplate.CreateContent() as ViewCell;
                        cell.BindingContext = Element.ItemsSource.Cast<IEnumerable>().First();

                        //Set the reference size accordingly.
                        flowLayout.HeaderReferenceSize = new CGSize(Element.Width, cell.RenderHeight);
                    }

                    _gridCollectionView.ReloadData();
                    _gridCollectionView.Delegate = new GridViewDelegate(ItemSelected, HandleOnScrolled);
                }
                );
            }
        }

        //TODO this method/mechanism needs some more thought
        /// <summary>
        /// Scrolls the index of the to item with.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <param name="animated">If set to <c>true</c> animated.</param>
        public void ScrollToItemWithIndex(int index, bool animated)
        {
            if (_gridCollectionView != null && _gridCollectionView.NumberOfItemsInSection(0) > index)
            {
                var indexPath = NSIndexPath.FromRowSection(index, 0);
                InvokeOnMainThread(() =>
                {
                    _gridCollectionView.ScrollToItem(indexPath, UICollectionViewScrollPosition.Top, animated);
                });
            }
            else
            {
                _initialIndex = index;
            }
        }

        #endregion
    }

    #endregion

    #region GridCollectionView

    /// <summary>
    /// Class GridCollectionView.
    /// </summary>
    public class GridCollectionView : UICollectionView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridCollectionView"/> class.
        /// </summary>
        public GridCollectionView() : this(default(CGRect))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridCollectionView"/> class.
        /// </summary>
        /// <param name="frm">The FRM.</param>
        public GridCollectionView(CGRect frm) : base(default(CGRect), new UICollectionViewFlowLayout() { })
        {
            AutoresizingMask = UIViewAutoresizing.All;
            ContentMode = UIViewContentMode.ScaleToFill;

            //Very important to set row and columnspacing to zero
            //as otherwise the cells will not fill.
            RowSpacing = 0;
            ColumnSpacing = 0;

            RegisterClassForCell(typeof(GridViewCell), new NSString(GridViewCell.Key));
            RegisterClassForSupplementaryView(typeof(GridViewCell), UICollectionElementKindSection.Header, new NSString(GridViewCell.HeaderKey));
        }

        /// <summary>
        /// Draws the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        public override void Draw(CGRect rect)
        {
            CollectionViewLayout.InvalidateLayout();

            base.Draw(rect);
        }

        /// <summary>
        /// Gets or sets the row spacing.
        /// </summary>
        /// <value>The row spacing.</value>
        public double RowSpacing
        {
            get
            {
                return (double)(CollectionViewLayout as UICollectionViewFlowLayout).MinimumLineSpacing;
            }
            set
            {
                (CollectionViewLayout as UICollectionViewFlowLayout).MinimumLineSpacing = (float)value;
            }
        }

        /// <summary>
        /// Gets or sets the column spacing.
        /// </summary>
        /// <value>The column spacing.</value>
        public double ColumnSpacing
        {
            get
            {
                return (double)(CollectionViewLayout as UICollectionViewFlowLayout).MinimumInteritemSpacing;
            }
            set
            {
                (CollectionViewLayout as UICollectionViewFlowLayout).MinimumInteritemSpacing = (float)value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the item.
        /// </summary>
        /// <value>The size of the item.</value>
        public CGSize ItemSize
        {
            get
            {
                return (CollectionViewLayout as UICollectionViewFlowLayout).ItemSize;
            }
            set
            {
                (CollectionViewLayout as UICollectionViewFlowLayout).ItemSize = value;
            }
        }
    }

    #endregion

    #region GridViewCell

    /// <summary>
	/// Class GridViewCell.
	/// </summary>
	public class GridViewCell : UICollectionViewCell
    {
        #region Fields

        UIView _view;
        object _originalBindingContext;
        FastGridCell _viewCell;

        CGSize _lastSize;

        /// <summary>
        /// The key
        /// </summary>
        public const string Key = "GridViewCell";

        /// <summary>
        /// The key
        /// </summary>
        public const string HeaderKey = "Header";

        //private static PropertyInfo _platform;
        //public static PropertyInfo PlatformProperty
        //{
        //    get
        //    {
        //        return _platform ?? (
        //            _platform = typeof(Element).GetProperty("Platform", BindingFlags.NonPublic | BindingFlags.Instance));
        //    }
        //}

        #endregion

        #region Properties

        public FastGridCell ViewCell { get { return _viewCell; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GridViewCell"/> class.
        /// </summary>
        /// <param name="frame">The frame.</param>
        [Export("initWithFrame:")]
        public GridViewCell(CGRect frame) : base(frame)
        {
            var a = NSBundle.MainBundle.LoadNib("statelist_item_background", null, null);
            SelectedBackgroundView = ObjCRuntime.Runtime.GetNSObject<UIView>(a.ValueAt(0));
        }

        #endregion

        #region Methods

        public void RecycleCell(object data, DataTemplate dataTemplate, VisualElement parent)
        {
            if (_viewCell == null)
            {
                _viewCell = (dataTemplate.CreateContent() as FastGridCell);

                //reflection method of setting isplatformenabled property
                // We are going to re - set the Platform here because in some cases (headers mostly) its possible this is unset and
                //   when the binding context gets updated the measure passes will all fail.By applying this here the Update call
                // further down will result in correct layouts.
                //var p = PlatformProperty.GetValue(parent);
                //PlatformProperty.SetValue(_viewCell, p);

                _viewCell.BindingContext = data;
                _viewCell.Parent = parent;
                _viewCell.PrepareCell(new Size(Bounds.Width, Bounds.Height));
                _originalBindingContext = _viewCell.BindingContext;
                var renderer = Platform.CreateRenderer(_viewCell.View); //RendererFactory.GetRenderer (_viewCell.View);
                _view = renderer.NativeView;

                _view.AutoresizingMask = UIViewAutoresizing.All;
                _view.ContentMode = UIViewContentMode.ScaleToFill;

                ContentView.AddSubview(_view);
                return;
            }
            else if (data == _originalBindingContext)
            {
                _viewCell.BindingContext = _originalBindingContext;
            }
            else
            {
                _viewCell.BindingContext = data;
            }
        }



        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if (_lastSize.Equals(CGSize.Empty) || !_lastSize.Equals(Frame.Size))
            {

                _viewCell.View.Layout(Frame.ToRectangle());
                _viewCell.OnSizeChanged(new Xamarin.Forms.Size(Frame.Size.Width, Frame.Size.Height));
                _lastSize = Frame.Size;
            }

            _view.Frame = ContentView.Bounds;
        }

        #endregion
    }

    #endregion

    #region GridDataSource

    /// <summary>
	/// Class GridDataSource.
	/// </summary>
	public class GridDataSource : UICollectionViewDataSource
    {
        #region Fields

        /// <summary>
        /// Delegate OnGetCell
        /// </summary>
        /// <param name="collectionView">The collection view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <returns>UICollectionViewCell.</returns>
        public delegate UICollectionViewCell OnGetCell(UICollectionView collectionView, NSIndexPath indexPath);

        /// <summary>
        /// Delegate OnRowsInSection
        /// </summary>
        /// <param name="collectionView">The collection view.</param>
        /// <param name="section">The section.</param>
        /// <returns>System.Int32.</returns>
        public delegate int OnRowsInSection(UICollectionView collectionView, nint section);

        /// <summary>
        /// Delegate OnItemSelected
        /// </summary>
        /// <param name="collectionView">The collection view.</param>
        /// <param name="indexPath">The index path.</param>
        public delegate void OnItemSelected(UICollectionView collectionView, NSIndexPath indexPath);

        /// <summary>
        /// Delegate OnItemSelected
        /// </summary>
        /// <param name="collectionView">The collection view.</param>
        /// <param name="indexPath">The index path.</param>
        public delegate int OnNumberOfSections(UICollectionView collectionView);

        public delegate UICollectionReusableView OnGetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath);

        /// <summary>
        /// The _on get cell
        /// </summary>
        private readonly OnGetCell _onGetCell;

        /// <summary>
        /// The _on rows in section
        /// </summary>
        private readonly OnRowsInSection _onRowsInSection;

        /// <summary>
        /// The _on item selected
        /// </summary>
        private readonly OnItemSelected _onItemSelected;

        /// <summary>
        /// The _on item selected
        /// </summary>
        private readonly OnNumberOfSections _onNumberOfSections;

        /// <summary>
        /// The _on item selected
        /// </summary>
        private readonly OnGetViewForSupplementaryElement _onGetViewForSupplementaryElement;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GridDataSource"/> class.
        /// </summary>
        /// <param name="onGetCell">The on get cell.</param>
        /// <param name="onRowsInSection">The on rows in section.</param>
        /// <param name="onItemSelected">The on item selected.</param>
        public GridDataSource(OnGetCell onGetCell,
            OnRowsInSection onRowsInSection,
            OnItemSelected onItemSelected,
            OnNumberOfSections onNumberOfSections,
            OnGetViewForSupplementaryElement onGetViewForSupplementaryElement)
        {
            _onGetCell = onGetCell;
            _onRowsInSection = onRowsInSection;
            _onItemSelected = onItemSelected;
            _onNumberOfSections = onNumberOfSections;
            _onGetViewForSupplementaryElement = onGetViewForSupplementaryElement;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the items count.
        /// </summary>
        /// <param name="collectionView">The collection view.</param>
        /// <param name="section">The section.</param>
        /// <returns>System.Int32.</returns>
        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return _onRowsInSection(collectionView, section);
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return _onNumberOfSections(collectionView);
        }

        public override UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            return _onGetViewForSupplementaryElement(collectionView, elementKind, indexPath);
        }

        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <param name="collectionView">The collection view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <returns>UICollectionViewCell.</returns>
        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {

            //tapGestureRecognizer.cancelsTouchesInView = NO;
            return _onGetCell(collectionView, indexPath);
        }

        #endregion
    }

    #endregion

    #region GridViewDelegate

    /// <summary>
    /// Class GridViewDelegate.
    /// </summary>
    public class GridViewDelegate : UICollectionViewDelegate
    {
        #region Fields

        /// <summary>
        /// Delegate OnItemSelected
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        public delegate void OnItemSelected(UICollectionView tableView, NSIndexPath indexPath);

        public delegate void OnScrolled(CGPoint contentOffset);

        /// <summary>
        /// The _on item selected
        /// </summary>
        private readonly OnItemSelected _onItemSelected;
        private readonly OnScrolled _onScrolled;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GridViewDelegate"/> class.
        /// </summary>
        /// <param name="onItemSelected">The on item selected.</param>
        public GridViewDelegate(OnItemSelected onItemSelected, OnScrolled onScrolled)
        {
            _onItemSelected = onItemSelected;
            _onScrolled = onScrolled;
        }

        #endregion

        #region Highlighting

        //To implement highlighting, the ItemHighlighted and ItemUnhighlighted methods of the UICollectionViewDelegate can be used.
        //For example, the following code will apply a yellow background of the ContentView when the Cell is highlighted, 
        //and a white background when un-highlighted.
        //public override void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        //{
        //    var cell = collectionView.CellForItem(indexPath);
        //    cell.ContentView.BackgroundColor = UIColor.Yellow;
        //}

        //public override void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath)
        //{
        //    var cell = collectionView.CellForItem(indexPath);
        //    cell.ContentView.BackgroundColor = UIColor.White;
        //}

        #endregion

        #region DisableSelection

        ///// <summary>
        ///// Selection is enabled by default in UICollectionView. 
        ///// To disable selection, override ShouldHighlightItem and return false as shown below:
        ///// </summary>
        ///// <param name="collectionView"></param>
        ///// <param name="indexPath"></param>
        ///// <returns></returns>
        //public override bool ShouldHighlightItem(UICollectionView collectionView, NSIndexPath indexPath)
        //{
        //    return false;
        //}

        ///// <summary>
        ///// When highlighting is disabled, the process of selecting a cell is disabled as well. 
        ///// Additionally, there is also a ShouldSelectItem method that controls selection directly, 
        ///// although if ShouldHighlightItem is implemented and returns false, ShouldSelectItem is not called.
        ///// ShouldSelectItem allows selection to be turned on or off on an item-by-item basis,
        ///// when ShouldHighlightItem is not implemented.It also allows highlighting without selection, 
        ///// if ShouldHighlightItem is implemented and returns true, while ShouldSelectItem returns false.
        ///// </summary>
        ///// <param name="collectionView"></param>
        ///// <param name="indexPath"></param>
        ///// <returns></returns>
        //public override bool ShouldSelectItem(UICollectionView collectionView, NSIndexPath indexPath)
        //{
        //    return false;
        //}

        #endregion

        #region Methods

        /// <summary>
        /// Items the selected.
        /// </summary>
        /// <param name="collectionView">The collection view.</param>
        /// <param name="indexPath">The index path.</param>
        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            _onItemSelected(collectionView, indexPath);
           
        }

        public override void Scrolled(UIScrollView scrollView)
        {
            _onScrolled(scrollView.ContentOffset);
        }

        #endregion
    }

    #endregion
}
