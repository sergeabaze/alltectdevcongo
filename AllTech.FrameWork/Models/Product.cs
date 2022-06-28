﻿using System;
using System.Collections.Generic;

namespace AllTech.FrameWork.Models
{
    public class Product : ObservableModel
	{

        #region Constructors
        public Product()
        {
            _sales = new List<Sale>();
        }
        public Product(string name, string id)
        {
            _sales = new List<Sale>();
            this.Name = name;
            this.Id = id;
        }
        protected Product(int salesCount)
        {
            _sales = new List<Sale>();
            _sales = SalesDataGenerator.GenerateSales(salesCount);
            _revenue = 0;

            foreach (Sale sale in _sales)
            {
                _revenue += sale.Value;
            }
        }

        #endregion
        #region Properties
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        public string SKU
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    this.OnPropertyChanged("SKU");
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private string _category;
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    this.OnPropertyChanged("Category");
                }
            }
        }

        private string _designer;
        public string Designer
        {
            get
            {
                return _designer;
            }
            set
            {
                if (_designer != value)
                {
                    _designer = value;
                    this.OnPropertyChanged("Designer");
                }
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    this.OnPropertyChanged("ImageUrl");
                }
            }
        }

        private bool _inStock;
        public bool InStock
        {
            get
            {
                return _inStock;
            }
            set
            {
                if (_inStock != value)
                {
                    _inStock = value;
                    this.OnPropertyChanged("InStock");
                }
            }
        }

        private bool _onBackOrder;
        public bool OnBackOrder
        {
            get
            {
                return _onBackOrder;
            }
            set
            {
                if (_onBackOrder != value)
                {
                    _onBackOrder = value;
                    this.OnPropertyChanged("OnBackOrder");
                }
            }
        }

        private string _supplier;
        public string Supplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                if (_supplier != value)
                {
                    _supplier = value;
                    this.OnPropertyChanged("Supplier");
                }
            }
        }

        private double _unitPrice;
        public double UnitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;
                    this.OnPropertyChanged("UnitPrice");
                }
            }
        }

        public double Price
        {
            get
            {
                return this.UnitPrice;
            }
            set
            {
                if (this.UnitPrice != value)
                {
                    this.UnitPrice = value;
                    this.OnPropertyChanged("Price");
                }
            }
        }

        private int _unitsInStock;
        public int UnitsInStock
        {
            get
            {
                return _unitsInStock;
            }
            set
            {
                if (_unitsInStock != value)
                {
                    _unitsInStock = value;
                    this.OnPropertyChanged("UnitsInStock");
                }
            }
        }

        private int _unitsOnOrder;
        public int UnitsOnOrder
        {
            get
            {
                return _unitsOnOrder;
            }
            set
            {
                if (_unitsOnOrder != value)
                {
                    _unitsOnOrder = value;
                    this.OnPropertyChanged("UnitsOnOrder");
                }
            }
        }

        private string _quantityPerUnit;
        public string QuantityPerUnit
        {
            get
            {
                return _quantityPerUnit;
            }
            set
            {
                if (_quantityPerUnit != value)
                {
                    _quantityPerUnit = value;
                    this.OnPropertyChanged("QuantityPerUnit");
                }
            }
        }

        private Uri _productUri;
        public Uri ProductUri
        {
            get
            {
                return _productUri;
            }
            set
            {
                _productUri = value;
                this.OnPropertyChanged("ProductUri");
            }
        }

        private double _revenue;
        public double Revenue
        {
            get
            {
                return _revenue;
            }
            set
            {
                if (_revenue != value)
                {
                    _revenue = value;
                    this.OnPropertyChanged("Revenue");
                }
            }
        }

        private List<Sale> _sales;
        public List<Sale> Sales
        {
            get
            {
                return _sales;
            }
            set
            {
                if (_sales != value)
                {
                    _sales = value;
                    this.OnPropertyChanged("Sales");
                }
            }
        } 
        #endregion
	}

}
