import {Component} from "../component.js";

export default class Icon extends Component {
    constructor() {
        super();
    }

    markup() {
        return `
            <img src="${this.getAttribute('src')}" alt="${this.getAttribute('alt') || 'Empty'}"/>
        `;
    }

    styles() {
        return `
            <style>
                :host {
                    display: inline-block;
                    width: ${this.getAttribute('data-size') || 1}rem;
                    height: ${this.getAttribute('data-size') || 1}rem;
                }
            
                img {
                    width: ${this.getAttribute('data-size') || 1}rem;
                    height: ${this.getAttribute('data-size') || 1}rem;
                }
            </style>
        `;
    }
}

customElements.define('a-icon', Icon);