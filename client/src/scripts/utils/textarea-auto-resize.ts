export function setTextareaHeight(tx: HTMLTextAreaElement | null) {
    if (tx === null) {
        return;
    }

    tx.style.height = '0';
    tx.style.height = `${tx.scrollHeight}px`;
}

export function onTextareaValueChanged(e: Event) {
    setTextareaHeight(e.target as HTMLTextAreaElement);
}
