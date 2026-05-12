// ===== TÉLÉCHARGEMENT FICHIER (PDF etc.) =====
function downloadFile(bytesBase64, fileName, mimeType) {
    const binaryString = atob(bytesBase64);
    const bytes = new Uint8Array(binaryString.length);
    for (let i = 0; i < binaryString.length; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }
    const blob = new Blob([bytes], { type: mimeType });
    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
}

// Alias pour PDF
window.downloadPdf = (base64, fileName) => {
    downloadFile(base64, fileName, 'application/pdf');
};

// Imprimer la page
window.printPage = () => {
    window.print();
};

// Fermer alertes automatiquement
document.addEventListener('DOMContentLoaded', () => {
    setTimeout(() => {
        document.querySelectorAll('.alert-auto-close').forEach(el => {
            el.style.transition = 'opacity 0.5s';
            el.style.opacity = '0';
            setTimeout(() => el.remove(), 500);
        });
    }, 4000);
});