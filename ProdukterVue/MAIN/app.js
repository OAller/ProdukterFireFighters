new Vue({
    el: '#app',
    data: {
        products: [],
        cart: []
    },
    created() {
        this.fetchProducts();
        this.fetchCart();
    },
    methods: {
        fetchProducts() {
            axios.get('/api/produkter/products')
                .then(response => {
                    this.products = response.data;
                })
                .catch(error => {
                    console.error("Fejl ved hentning af produkter:", error);
                });
        },
        fetchCart() {
            axios.get('/api/produkter/cart')
                .then(response => {
                    this.cart = response.data;
                })
                .catch(error => {
                    console.error("Fejl ved hentning af kurv:", error);
                });
        },
        addToCart(productId) {
            axios.post(`/api/produkter/cart/add/${productId}`)
                .then(response => {
                    this.cart = response.data.cart;
                })
                .catch(error => {
                    console.error("Fejl ved tilføjelse til kurv:", error);
                });
        },
        removeFromCart(productId) {
            axios.delete(`/api/produkter/cart/remove/${productId}`)
                .then(response => {
                    this.cart = response.data.cart;
                })
                .catch(error => {
                    console.error("Fejl ved fjernelse fra kurv:", error);
                });
        }
    }
});
