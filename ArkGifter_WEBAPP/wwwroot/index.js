//how to format line comment in JS
/* how to format block comment in JS */

function ArkGifter_WEBAPP() {
  // Get elements
  const giftBasketsBtn = document.getElementById('giftBasketsBtn');
  const homeBtn = document.getElementById('homeBtn');
  const arkansasProductsBtn = document.getElementById('arkansasProductsBtn');
  const localVendorsBtn = document.getElementById('localVendorsBtn');

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

  if (localVendorsBtn) {
    localVendorsBtn.addEventListener('click', function () {
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

}
// Call the ArkGifter_WEBAPP function
ArkGifter_WEBAPP();
