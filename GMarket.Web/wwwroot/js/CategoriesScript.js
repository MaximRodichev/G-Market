document.addEventListener("DOMContentLoaded", () => {
    
    document.getElementById("adult-min-price").addEventListener("input", updatePriceValues);
    document.getElementById("adult-max-price").addEventListener("input", updatePriceValues);
    
    
    
    function updatePriceValues() {
        const adultMin = document.getElementById('adult-min-price').value;
        const adultMax = document.getElementById('adult-max-price').value;
         document.getElementById('adult-price-values').innerText = `${adultMin} - ${adultMax}`
        
    }
    
    
    const sortSelect = document.getElementById('sort-options');
    const productsContainer = document.querySelector('.list-products');
    
    sortSelect.addEventListener('change', e => {
        const sortOptions = sortSelect.value;
        
        const products = Array.from(productsContainer.querySelectorAll('.product-item'));
        
        products.sort((a,b)=>{
            switch(sortOptions){
                case 'price-adult-asc': {
                    const priceA = parseFloat(a.querySelector('.price').textContent
                        .replace(" RUB", "")
                        .replace("Цена:", ""));
                    const priceB = parseFloat(b.querySelector('.price').textContent
                        .replace(" RUB", "")
                        .replace("Цена:", ""));
                    return priceA - priceB;
                }
                case 'price-adult-desc': {
                    const priceA = parseFloat(a.querySelector('.price').textContent
                        .replace(" RUB", "")
                        .replace("Цена:", ""));
                    const priceB = parseFloat(b.querySelector('.price').textContent
                        .replace(" RUB", "")
                        .replace("Цена:", ""));
                    return priceB - priceA;
                }
                default:
                    location.reload();
            }
        })
        
        products.forEach(product => productsContainer.appendChild(product));
        
    })
    
    document.getElementById("apply-filter").addEventListener("click", ()=>{
        // Получаем все чекбоксы внутри контейнера .brand-type
        const checkboxes = document.querySelectorAll('.brand-type .custom-checkbox');
        
        // Функция для получения текущих состояний всех чекбоксов
        function getCheckboxStates() {
            const states = Array.from(checkboxes).map(checkbox => ({
                label: checkbox.nextElementSibling.innerText, // Получаем текст связанного label
                value: checkbox.value, // Значение чекбокса
                checked: checkbox.checked // Состояние чекбокса
            }));
            console.log(states);
            return states;
        }
        const adultMin = document.getElementById('adult-min-price').value;
        const adultMax = document.getElementById('adult-max-price').value;
        const categories = getCheckboxStates();
        const categoryValues = categories
            .filter(category => category.checked) // Оставляем только отмеченные чекбоксы
            .map(category => category.value);
        const filterData = {
            Brands: categoryValues,
            PriceMax: parseFloat(adultMax) || 5000,
            PriceMin: parseFloat(adultMin) || 0,
        }
        
        fetch("/Categories/ListOfProductByFilter/", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(filterData),
        })
            .then(response => {
                if(!response.ok){
                    throw new Error("Ошибка фильтрации");
                }
                return response.json();
            })
            .then((data)=>{
                dataDisplay(data);
            })
            .catch((error)=>{
                console.error("Ошибка: ", error);
            });
    
    })

    function dataDisplay(data){
        const productList = document.querySelector(".list-products");
        productList.innerHTML = '';
        
        if(!data || data.length === 0){
            const noProductMessage =  `<h2>По данному фильтру ничего нет</h2>`;
            productList.innerHTML = noProductMessage;
        }else{
            const products = Array.isArray(data) ? data : data.$values;
            products.forEach((product)=>{
                const productItem = `
                <div class="product-item">
                        <img src=${product.image} class="item-product-image"/>
                        <div class="item-info">
                            <h2>${product.name}</h2>
                            <h6 class="price">Цена: ${product.price} RUB</h6>
                        </div>
                    </div>`
                productList.innerHTML += productItem;
            })
        }
    }
})