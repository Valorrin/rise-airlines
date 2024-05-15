const tableElement = document.querySelector("table");


let contentOpacity = 0;

const fadeIn = setInterval(() => {
    tableElement.style.opacity = contentOpacity;
    contentOpacity += 0.01;
    if (contentOpacity >= 1) {
        clearInterval(fadeIn);

    }
}, 10);