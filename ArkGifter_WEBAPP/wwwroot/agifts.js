
//currently a WIP.  but this code will handle the button click to create a gift basket, the original goal of this project
//it will need to show users a list of products, let them select a few products, then execute the create function
//that will need to send the product IDs of the selected products to populate a row in the basketdetails table in SQL
//then it will need to create a row in the basket


// Define an array to store the selected product IDs


function handleCreateBasket() {
    console.log('Create Basket button clicked');
    // Function to post a new gift basket (empty)
    createGiftBasket()
    // Function to select products from a list and create a basketdetails item with all selected products, and the same basketID as the previous function
  
    //calls the table
    // productCheckboxTable();
  }
  
  function createGiftBasket() {
    // Prompt the user for the name and summary of the gift basket
    const basketName = prompt("Enter the name of the gift basket:");
    const basketSummary = prompt("Enter the summary of the gift basket:");
  
    // Create the request body with the entered values
    const requestBody = {
      basket_name: basketName,
      basket_summary: basketSummary
    };
  
    // Send the POST request to the API endpoint
    fetch('http://localhost:5226/api/giftbaskets', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(requestBody)
    })
    .then(response => {
      if (response.ok) {
        alert('Gift basket created successfully!');
      } else {
        alert('Failed to create the gift basket.');
      }
    })
    .catch(error => {
      console.error('Error:', error);
      alert('An error occurred while creating the gift basket.');
    });
  }
  


  function productCheckboxTable() {
    fetch('http://localhost:5226/api/product/arkansas')
      .then(response => response.json())
      .then(data => {
        data.sort((a, b) => a.maker.localeCompare(b.maker));
  
        // Clear the table body
        const tableBody = productTable.querySelector('tbody');
        tableBody.innerHTML = '';
  
        // Iterate over the fetched data and create table rows
        data.forEach(product => {
          const row = document.createElement('tr');
  
          // Create checkbox cell
          const checkboxCell = document.createElement('td');
          const checkbox = document.createElement('input');
          checkbox.type = 'checkbox';
          checkbox.value = product.productID;
          checkboxCell.appendChild(checkbox);
          row.appendChild(checkboxCell);
  
          const productIDCell = document.createElement('td');
          productIDCell.textContent = product.productID;
          row.appendChild(productIDCell);
  
          const makerCell = document.createElement('td');
          makerCell.textContent = product.maker;
          row.appendChild(makerCell);
  
          const productCell = document.createElement('td');
          productCell.textContent = product.product;
          row.appendChild(productCell);
  
          const priceCell = document.createElement('td');
          priceCell.textContent = product.price ? '$' + product.price.toFixed(2) : '';
          row.appendChild(priceCell);
  
          tableBody.appendChild(row);
        });
  
        // Check if table head exists and add it if not
        if (!productTable.querySelector('thead')) {
          const headerRow = document.createElement('tr');
          ['ProductID', 'Maker', 'Product', 'Price'].forEach(column => {
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
  





// Function to handle the click event for the Update Basket button
function handleUpdateBasket() {
    // TODO: Implement the logic for updating an existing gift basket
    console.log('Update Basket button clicked');
}

// Function to handle the click event for the Delete Basket button
function handleDeleteBasket() {
    // TODO: Implement the logic for deleting a gift basket
    console.log('Delete Basket button clicked');
}

// Add event listeners for the CRUD navigation buttons
var createBasketBtn = document.getElementById('createBasketBtn');
createBasketBtn.addEventListener('click', handleCreateBasket);

var updateBasketBtn = document.getElementById('updateBasketBtn');
updateBasketBtn.addEventListener('click', handleUpdateBasket);

var deleteBasketBtn = document.getElementById('deleteBasketBtn');
deleteBasketBtn.addEventListener('click', handleDeleteBasket);



//script from index.js.  its reduplicated code but i only learned how to orient objects in csharp 
// const giftBasketsBtn = document.getElementById('giftBasketsBtn');
const homeBtn = document.getElementById('homeBtn');
const arkansasProductsBtn = document.getElementById('arkansasProductsBtn');
const vendorsBtn = document.getElementById('vendorsBtn');
// if (giftBasketsBtn) {
//     giftBasketsBtn.addEventListener('click', function () {
//       window.location.href = 'agifts.html';
//     });
//   }

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

//READING IS FUNDAMENTAL
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