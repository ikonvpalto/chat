import { Observable } from "../misc/observable.ts";

export class Store<T> extends Observable {
    private _data: T;

    get data() : Readonly<T> {
        return Object.freeze(this._data);
    }

    protected set data(value: T) {
        this._data = value;
        this.notifySubscribers(value);
    }

    protected constructor(initData: T) {
        super();
        this._data = initData;
    }
}
