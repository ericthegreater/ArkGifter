function ArkGifter_Vendors() {
      // Get elements
  const giftBasketsBtn = document.getElementById('giftBasketsBtn');
  const homeBtn = document.getElementById('homeBtn');
  const arkansasProductsBtn = document.getElementById('arkansasProductsBtn');

  // Add event listeners

  // Functions

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

 // Function to fetch vendors and populate the table
 function fetchVendors() {
    fetch('http://localhost:5226/api/vendor')
      .then(response => response.json())
      .then(vendors => {
        const vendorsTableBody = document.getElementById('vendorsTable');

        // Clear the vendorsTableBody before populating with new data
        vendorsTableBody.querySelector('tbody').innerHTML = '';

        // Loop through the vendors and create table rows for each vendor
        vendors.forEach(vendor => {
          const row = document.createElement('tr');

          const vendorNameCell = document.createElement('td');
          vendorNameCell.textContent = vendor.vendor_name;
          row.appendChild(vendorNameCell);

          const vendorCityCell = document.createElement('td');
          vendorCityCell.textContent = vendor.vendor_city;
          row.appendChild(vendorCityCell);

          const separateDistributorCell = document.createElement('td');
          separateDistributorCell.textContent = vendor.separate_distributor ? 'Yes' : 'No';
          row.appendChild(separateDistributorCell);

          const distributorCell = document.createElement('td');
          distributorCell.textContent = vendor.distributor;
          row.appendChild(distributorCell);

          vendorsTableBody.querySelector('tbody').appendChild(row);
        });
      })
      .catch(error => {
        console.error('Error:', error);
      });
  };

  // Call the function to fetch and populate vendors table
  fetchVendors();
};
ArkGifter_Vendors();