// JSON verisi
const iconlar = [
    { id: 1, name: "bi bi-box-fill" },
    { id: 2, name: "bi bi-brilliance" },
    { id: 3, name: "bi bi-browser-firefox" }
];

// Select elementini seç
const selectElement = document.getElementById('icons');

// JSON verisini kullanarak seçenekleri oluştur ve select elementine ekle
iconlar.forEach(icon => {
    const option = document.createElement('option');
    option.value = icon.name;
    option.textContent = icon.displayName; // Görüntülenecek metni ayarlayın
    option.setAttribute('data-icon', icon.name); // İkon sınıfını data-attribute olarak ekleyin
    selectElement.appendChild(option);
});