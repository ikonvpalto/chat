import {LitElement, css, html} from 'lit';
import {customElement, property} from 'lit/decorators.js';

// ToDo: very bad idea (it was the first one), need to be refactored
type Size = 'small' | 'medium' | 'large';

/**
 * Icon component
 */
@customElement('an-icon')
export class Icon extends LitElement {
    @property({attribute: 'data-src'})
    src: string = '';

    @property({attribute: 'data-alt'})
    alt: string = '';

    @property({attribute: 'data-size'})
    size: Size = 'medium';

    render() {
        return html`
            <img src=${this.src} alt=${this.alt} class=${this.size}/>
        `;
    }

    static styles = css`
        :host {
            display: inline-block;
            width: fit-content;
            height: fit-content;
        }

        img.small {
            width: 1.25rem;
            height: 1.25rem;
        }

        img.medium {
            width: 2.25rem;
            height: 2.25rem;
        }

        img.large {
            width: 3.25rem;
            height: 3.25rem;
        }
    `;
}
