﻿using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Constants;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult();
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),true,"Ürünler Listelendi");
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int Id)
        {
            return _productDal.GetAll(p => p.CategoryId == Id);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }

        public IDataResult<Product> GetById(int productId)
        {
            return _productDal.Get(p => p.ProductId == productId);
        }

        public IResult Add(Product product)
        {
            if (product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameİnvalid);
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }
    }
}
