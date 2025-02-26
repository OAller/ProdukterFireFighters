new Vue({
    el: '#app',
    data: {
      products: []
    },
    created() {
      axios.get('http://localhost:5209/api/Produkter/products')
        .then(response => {
          this.products = response.data;
        })
        .catch(error => {
          console.error("Der opstod en fejl ved hentning af produkter:", error);
        });
    }
  });
  