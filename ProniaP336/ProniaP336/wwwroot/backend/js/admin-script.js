const deleteBtns = document.querySelectorAll(".deleteBtn");

deleteBtns.forEach(deleteBtn => {
    deleteBtn.addEventListener("click", function (e) {
        e.preventDefault();

        let url = this.getAttribute("href");

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(url, { method: "POST" })
                    .then(response => response.json())
                    .then(data => {
                        Swal.fire({
                            title: "Deleted!",
                            text: data.message,
                            icon: "success"
                        }).then(result => {
                            location.reload();
                        });
                    })
               
            }
        });
    })
})