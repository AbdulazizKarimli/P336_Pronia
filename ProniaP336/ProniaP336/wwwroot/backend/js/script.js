const loadMoreBtn = document.getElementById("loadMoreBtn");
const productBox = document.getElementById("productBox");
const productCount = document.getElementById("productCount").value;
const productDetails = document.querySelectorAll(".product-detail");
const productModal = document.querySelector("#product-modal-content");

let skip = 8;
loadMoreBtn.addEventListener("click", function () {
    let url = `/Shop/LoadMore?skip=${skip}`;

    fetch(url).then(response => response.text())
        .then(data => productBox.innerHTML += data);

    skip += 8;

    if (skip >= productCount) {
        loadMoreBtn.remove();
    }
})

productDetails.forEach(productDetail => {
    productDetail.addEventListener("click", function (e) {
        e.preventDefault();
        console.log(productModal);

        let url = this.getAttribute("href");

        fetch(url).then(response => response.text())
            .then(data => {
                productModal.innerHTML = data;
            });
;    })
})