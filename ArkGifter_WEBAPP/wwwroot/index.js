//how to format line comment in JS
/* how to format block comment in JS */

function ArkGifter_WEBAPP() {
  // Get elements
  const giftBasketsBtn = document.getElementById('giftBasketsBtn');
  const homeBtn = document.getElementById('homeBtn');
  const arkansasProductsBtn = document.getElementById('arkansasProductsBtn');
  const vendorsBtn = document.getElementById('vendorsBtn');

  //add event listeners

  //Functions

  // Navigation Buttons

  if (giftBasketsBtn) {
    giftBasketsBtn.addEventListener('click', function () {
      window.location.href = 'agifts.html';
    });
  }

  if (homeBtn) {
    homeBtn.addEventListener('click', function () {
      window.location.href = 'index.html';
    });
  }

  if (arkansasProductsBtn) {
    arkansasProductsBtn.addEventListener('click', function () {
      window.location.href = 'aproducts.html';
    });
  }

  if (vendorsBtn) {
    vendorsBtn.addEventListener('click', function () {
      window.location.href = 'avendors.html';
      //i put this last bit up here but... its still not fetching the vendors
      fetchVendors();
    });
  }
  


  // GIFT BASKETS PAGE
  // Fetch gift baskets data from the API and display them on the page

  function displayGiftBaskets() {
    const giftBasketsContainer = document.getElementById('giftBasketsContainer');

    fetch('http://localhost:5226/api/giftbaskets')
      .then(response => response.json())
      .then(data => {
        const table = document.createElement('table');
        table.classList.add('gift-baskets-table');

        // Create table headers
        const headers = ['', 'Name', 'Summary', 'Total Cost'];
        const headerRow = document.createElement('tr');
        headers.forEach(header => {
          const th = document.createElement('th');
          th.textContent = header;
          headerRow.appendChild(th);
        });
        table.appendChild(headerRow);

        // Populate table rows with gift basket data
        data.forEach(giftBasket => {
          const row = document.createElement('tr');
          const { basketId, basketName, basketSummary, totalCost } = giftBasket;

          const idCell = document.createElement('td');
          idCell.textContent = basketId;
          row.appendChild(idCell);

          const nameCell = document.createElement('td');
          nameCell.textContent = basketName;
          row.appendChild(nameCell);

          const summaryCell = document.createElement('td');
          summaryCell.textContent = basketSummary;
          row.appendChild(summaryCell);

          const costCell = document.createElement('td');
          costCell.textContent = '$' + totalCost.toFixed(2);
          row.appendChild(costCell);

          table.appendChild(row);
        });

        giftBasketsContainer.appendChild(table);
      })
      .catch(error => {
        console.error('Error:', error);
      });
  }

  // Call the function to fetch and display gift baskets
  displayGiftBaskets();

  //code for products page

  function displayArkansasProducts() {
    const productTable = document.getElementById('productTable');
    const tableHead = productTable.querySelector('thead');

    fetch('http://localhost:5226/api/product/arkansas')
      .then(response => response.json())
      .then(data => {
        data.sort((a, b) => a.maker.localeCompare(b.maker));

        // Clear the table body
        productTable.querySelector('tbody').innerHTML = '';

        // Iterate over the fetched data and create table rows
        data.forEach(product => {
          const row = document.createElement('tr');

          const makerCell = document.createElement('td');
          makerCell.textContent = product.maker;
          row.appendChild(makerCell);

          const productCell = document.createElement('td');
          productCell.textContent = product.product;
          row.appendChild(productCell);

          const priceCell = document.createElement('td');
          priceCell.textContent = '$' + product.price.toFixed(2);
          row.appendChild(priceCell);

          productTable.appendChild(row);
        });

        // Check if table head exists and add it if not
        if (!tableHead) {
          const headerRow = document.createElement('tr');
          ['Maker', 'Product', 'Price'].forEach(column => {
            const th = document.createElement('th');
            th.textContent = column;
            headerRow.appendChild(th);
          });
          const newTableHead = document.createElement('thead');
          newTableHead.appendChild(headerRow);
          productTable.appendChild(newTableHead);
        }
      })
      .catch(error => {
        console.error('Error:', error);
      });
  }

  // Call the function to fetch and display Arkansas products
  displayArkansasProducts();

  //products page buttons
  // Function to handle the click event for the Create Product button
  function handleCreateProduct() {
    const maker = prompt('Enter the maker:');
    const product = prompt('Enter the product:');
    const price = parseFloat(prompt('Enter the price:'));

    //old way
    // const newProduct = {
    //   Maker: maker,
    //   Product: product,
    //   Price: price,
    // };
var url="http://localhost:5226/api/product/create?product=" + product + "&maker=" + maker + "&price=" + price;
    // Send a POST request to the API endpoint to create the product
    // fetch('http://localhost:5226/api/product', {
    //   method: 'POST',
    //   headers: {
    //     'Content-Type': 'application/json',
    //   },
    //   body: JSON.stringify(newProduct),
    // })
fetch(url, {method:'GET'})
      .then(response => {
        if (response.ok) {
          alert('Product created successfully.');
          // Perform any additional actions or refresh the product list
        } else {
          alert('Failed to create product.');
        }
      })
      .catch(error => {
        console.error('Error:', error);
        alert('An error occurred while creating the product.');
      });
  }

  // Function to handle the click event for the Update Product button
  function handleUpdateProduct() {
    // UPDATE LOGIC GOES HERE
    console.log('Update Product button clicked');
  }

  // Function to handle the click event for the Delete Product button
  function handleDeleteProduct() {
    const productName = document.getElementById('productNameInput').value;

    if (!productName) {
      alert('Please enter a product name.'); // Show an error message if the input field is empty
      return;
    }

    const confirmed = window.confirm(`Are you sure you want to delete the product ${productName}?`);
    if (!confirmed) {
      return; // Cancel the deletion if not confirmed
    }

    // Make an API request to delete the product
    fetch(`http://localhost:5226/api/product/${productName}`, {
      method: 'DELETE'
    })
      .then(response => {
        if (response.ok) {
          console.log(`Product ${productName} deleted successfully`);
          alert(`Product ${productName} deleted successfully`); // Show success message
        } else {
          console.log(`Failed to delete product ${productName}`);
          alert(`Failed to delete product ${productName}`); // Show error message
        }
      })
      .catch(error => {
        console.error('Error:', error);
        alert('An error occurred while deleting the product.');
      });
  }


  // Attach event listeners to the buttons
  document.getElementById('createProductBtn').addEventListener('click', handleCreateProduct);
  document.getElementById('updateProductBtn').addEventListener('click', handleUpdateProduct);
  document.getElementById('deleteProductBtn').addEventListener('click', handleDeleteProduct);


  //VENDOR CODE was going here but.... i spent 3 or 4 hours and it simply won't serve it even tho i can see the raw data and the JSON in the brwoser. 
// Function to fetch vendors and populate the table
// Function to fetch vendors and populate the table
const fetchVendors = () => {
  fetch('/api/vendor')
    .then(response => response.json())
    .then(vendors => {
      const vendorsTableBody = document.getElementById('vendorsTableBody');

      // Clear the vendorsTableBody before populating with new data
      vendorsTableBody.innerHTML = '';

      // Loop through the vendors and create table rows for each vendor
      vendors.forEach(vendor => {
        const row = document.createElement('tr');

        const vendorNameCell = document.createElement('td');
        vendorNameCell.textContent = vendor.Vendor_Name;
        row.appendChild(vendorNameCell);

        const vendorCityCell = document.createElement('td');
        vendorCityCell.textContent = vendor.Vendor_City;
        row.appendChild(vendorCityCell);

        const separateDistributorCell = document.createElement('td');
        separateDistributorCell.textContent = vendor.Separate_Distributor ? 'Yes' : 'No';
        row.appendChild(separateDistributorCell);

        const distributorCell = document.createElement('td');
        distributorCell.textContent = vendor.Distributor;
        row.appendChild(distributorCell);

        vendorsTableBody.appendChild(row);
      });
    })
    .catch(error => {
      console.error('Error:', error);
    });
};


  

}
// Call the ArkGifter_WEBAPP function
ArkGifter_WEBAPP();
