//how to format line comment in JS
/* how to format block comment in JS */

function ArkGifter_WEBAPP() {

  //get elements

  //add event listeners

  //Functions

  //gift basket button
  document.addEventListener('DOMContentLoaded', function () {
    const giftBasketsBtn = document.getElementById('giftBasketsBtn');

    giftBasketsBtn.addEventListener('click', function () {
      // Redirect to the giftbaskets.html page
      window.location.href = 'giftbaskets.html';
    });

    // Call the function to fetch and display gift baskets
    displayGiftBaskets();
  });

//GIFT BASKETS PAGE
// Fetch gift baskets data from the API and display them on the page
function displayGiftBaskets() {
  const giftBasketsContainer = document.getElementById('giftBasketsContainer');

  fetch('http://localhost:5226/api/giftbaskets')
    .then(response => response.json())
    .then(data => {
      const table = document.createElement('table');
      table.classList.add('gift-baskets-table');

      // Create table headers
      const headers = ['', 'Name', 'Summary', 'Price'];
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
        Object.values(giftBasket).forEach(value => {
          const td = document.createElement('td');
          td.textContent = value;
          row.appendChild(td);
        });
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


  };



// DOMContentLoaded event ensures the JavaScript code runs when the page is loaded
ArkGifter_WEBAPP();