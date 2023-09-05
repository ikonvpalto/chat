import {LitElement, css, html} from 'lit';
import {customElement, property} from 'lit/decorators.js';
import {PropertyDeclaration} from '@lit/reactive-element';
import {classMap} from 'lit/directives/class-map.js';

// ToDo: very bad idea (it was the first one), need to be refactored
type Size = 'small' | 'medium' | 'large';

type Modifier = 'round' | 'filled' ;

function getButtonModifiersFromString(value: string): Modifier[] {
    return value
        .split(' ')
        .map(v => v as Modifier);
}

function convertButtonModifiersToString(value: Modifier[]): string {
    return value
        .join(' ');
}

/**
 * Button component
 *
 * @slot - slot for button content - icon or text or both
 */
@customElement('a-button')
export class Button extends LitElement {

    static modifiersOptions: PropertyDeclaration<Modifier[]> = {
        type: Array,
        attribute: 'data-modifiers',
        converter: {
            toAttribute: convertButtonModifiersToString,
            fromAttribute: getButtonModifiersFromString,
        }
    };
    @property(Button.modifiersOptions)
    modifiers: Array<Modifier> = [];

    @property({type: Boolean})
    disabled = false;

    @property({attribute: 'data-size'})
    size: Size = 'medium';

    render() {
        const buttonClasses = {
            button: true,
            round: this.modifiers.includes('round'),
            filled: this.modifiers.includes('filled'),
            [this.size]: true
        };

        return html`
            <button ${this.disabled ? 'disabled' : ''} class=${classMap(buttonClasses)}>
                <slot></slot>
            </button>
        `;
    }

    static styles = css`
        :host {
            display: contents;
        }

        .button.small {
            --content-font-size: 1.25rem;
        }

        .button.medium {
            --content-font-size: 2rem;
        }

        .button.large {
            --content-font-size: 3rem;
        }

        .button {
            --content-line-height: 1.5;
            --vertical-padding: .75rem;
            --border-radius: calc((var(--vertical-padding) * 2 + var(--content-font-size) * var(--content-line-height)) / 2);

            display: inline-block;
            background: none;
            border: none;
            margin: 0;
            cursor: pointer;
            padding: var(--vertical-padding) var(--border-radius);
            border-radius: var(--border-radius);

            height: calc(var(--border-radius) * 2);
            width: fit-content;
        }

        .button.round {
            padding: var(--vertical-padding);
            border-radius: 50%;
            aspect-ratio: 1;
            width: calc(var(--border-radius) * 2);
        }

        .filled {
            background: var(--color-accent-bg);
            color: var(--color-accent);
        }

        .button ::slotted(span) {
            font-size: var(--content-font-size, 1.25rem);
            line-height: var(--content-line-height, 1.5);
        }
    `;
}
