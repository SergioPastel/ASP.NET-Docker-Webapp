// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Initialize the start time
const startDate = Date.now();

function updateTimer() {
    const currentTime = Date.now();
    const elapsedTime = currentTime - startDate;

    // Calculate days, hours, minutes, and seconds
    const seconds = Math.floor(elapsedTime / 1000) % 60;
    const minutes = Math.floor(elapsedTime / (1000 * 60)) % 60;
    const hours = Math.floor(elapsedTime / (1000 * 60 * 60)) % 24;
    const days = Math.floor(elapsedTime / (1000 * 60 * 60 * 24));

    // Safely update the timer if it exists
    const timerEl = document.getElementById("timer");
    if (timerEl) {
        timerEl.textContent = `${days}d ${hours}h ${minutes}m ${seconds}s`;
    }

    // Safely update the current date/time if the element exists
    const dateEl = document.getElementById("current-date");
    if (dateEl) {
        const date = new Date();
        dateEl.textContent = date.toLocaleString();
    }
}

// Run immediately once so user doesn’t wait for 1s delay
updateTimer();

// Update every second
setInterval(updateTimer, 1000);
