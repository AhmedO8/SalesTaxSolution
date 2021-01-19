using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesTax.Data;
using SalesTax.Models;

namespace SalesTax.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MvcProductContext _context;
        public ProductsController(MvcProductContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
             return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Quantity,Imported,Price,Categories")] Product product)
        {

            if (ModelState.IsValid)
            {
                product.SalesTaxAmount = CalculateSalesTax(product);
                product.FinalProductPrice = CalculateFinalPrice(product);
                _context.Add(product);
                await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Quantity,Imported,Price,Categories")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private void CalculateTotals(List<Product> lists)
        {
        }


        private decimal CalculateFinalPrice(Product product)
        {
            var FinalProductPrice = product.SalesTaxAmount + product.Price;
            return FinalProductPrice;
        }

        private decimal CalculateSalesTax(Product product)
        {
            decimal basicSalesTaxRate = 10;
            decimal importSalesTaxRate = 5;
            decimal oneHundred = 100;
            decimal taxAmount = 0;

            //Scenario 1: Not essential and imported
            if (!product.ProductCategory.Equals(ProductCategories.Books)
                   || !product.ProductCategory.Equals(ProductCategories.Food)
                   || !product.ProductCategory.Equals(ProductCategories.Medical))
            {
                if (product.Imported)
                {
                    taxAmount = (product.Price * (basicSalesTaxRate + importSalesTaxRate)) / oneHundred;
                }
                else
                {
                    //Scenario 2: Not essential and not imported
                    taxAmount = (product.Price * basicSalesTaxRate) / oneHundred;
                }

            }
            else
            {
                //Scenario 3: Essential and imported
                taxAmount = (product.Price * importSalesTaxRate) / oneHundred;
            }
            taxAmount = RoundUp(taxAmount, 0.05m);
            return taxAmount;
        }

        private decimal RoundUp(decimal value, decimal step)
        {
            var multiple = Math.Ceiling(value / step);
            return step * multiple;
        }

    }
}
