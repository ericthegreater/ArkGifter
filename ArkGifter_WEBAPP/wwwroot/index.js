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

//VENDOR CODE was going here but.... i spent 3 or 4 hours and it simply won't serve it even tho i can see the raw data and the JSON in the brwoser. 





}
// Call the ArkGifter_WEBAPP function
ArkGifter_WEBAPP();
