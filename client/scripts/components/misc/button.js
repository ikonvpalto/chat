import {Component} from "../component.js";

export default class Button extends Component {
    constructor() {
        super();
    }

    registerEvents() {
        this.shadowRoot
            .querySelector('button')
            .addEventListener('click', this.onClick.bind(this));
    }

    onClick(event) {
        event.preventDefault();
        event.stopPropagation();
        this.fireEvent('click');
    }

    onClickFactory(mark) {
        return (event) => {
            event.preventDefault();
            event.stopPropagation();

            console.log(mark);

            this.fireEvent('click');
        }
    }

    markup() {
        return `
            <button
                ${this.hasAttribute('disabled') ? 'disabled' : ''}
                ${this.hasAttribute('type') ? this.getAttribute('type') : ''}
            >
                <slot name="content"></slot>
            </button>
        `;
    }

    styles() {
        return `
            <style>
                :host {
                    /*display: contents;*/
                }

                button {
                    display: inline-block;
                    background: none;
                    border: none;
                    margin: 0;
                    padding: .5rem;
                    cursor: pointer;
                    width: fit-content;
                    height: fit-content;
                }

                :host([data-type*="round"]) button {
                    border-radius: 50%;
                    aspect-ratio: 1;
                }

                :host([data-type*="filled"]) button {
                    background: var(--color-accent-bg);
                    color: var(--color-accent);
                }
            </style>
        `;
    }
}

customElements.define('a-button', Button);
