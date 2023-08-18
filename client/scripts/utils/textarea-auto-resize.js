function setTextareaHeight(tx) {
    tx.style.height = '0';
    tx.style.height = `${tx.scrollHeight}px`;
}

function onTextareaInput() {
    setTextareaHeight(this);
}

export default function setupTextareaAutoResize(root = document) {
    root
        .querySelectorAll('textarea')
        .forEach(tx => {
            setTextareaHeight(tx);
            tx.addEventListener('input', onTextareaInput)
        });
}
