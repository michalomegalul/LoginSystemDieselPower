window.addEventListener("DOMContentLoaded", function() {
    var tableBody = document.querySelector("#accountTable tbody");
  
    // Function to add a new row to the table
    function addRow(pcName, shareState, timeAdded, timeLeft) {
      var newRow = document.createElement("tr");
  
      newRow.innerHTML = `
        <td>${pcName}</td>
        <td>${shareState} <button class="delete-btn">Delete</button></td>
        <td>${timeAdded}</td>
        <td>${timeLeft}</td>
      `;
  
      // Add event listener to the delete button
      var deleteButton = newRow.querySelector(".delete-btn");
      deleteButton.addEventListener("click", function() {
        deleteRow(newRow);
      });
  
      tableBody.appendChild(newRow);
    }
  
    // Function to delete a row
    function deleteRow(row) {
      row.remove();
    }
  
    // Parse the existing table rows and extract data
    var existingRows = document.querySelectorAll("#accountTable tbody tr");
    existingRows.forEach(function(row) {
      var pcName = row.querySelector("td:first-child").textContent;
      var shareState = row.querySelector("td:nth-child(2)").textContent;
      var timeAdded = row.querySelector("td:nth-child(3)").textContent;
      var timeLeft = row.querySelector("td:nth-child(4)").textContent;
  
      addRow(pcName, shareState, timeAdded, timeLeft);
    });
  
    // Example of adding a new row dynamically
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
    addRow("John Doe", "Authorized", "May 25, 2023", "May 30, 2023");
  });
  function sendDataToCSharp(data) {
    fetch('MyNamedPipe', {
      method: 'POST',
      body: data
    })
      .then(response => response.text())
      .then(responseData => {
        console.log('Received response from C#: ' + responseData);
        
      })
      .catch(error => {
        console.error('Error sending data to C#: ' + error);
      });
  }
  
  // Example usage
  var dataToSend = 'Data from website';
  sendDataToCSharp(dataToSend);
  