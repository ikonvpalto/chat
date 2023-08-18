import Observable from "../misc/observable.js";

export default class Store extends Observable {
    get data() {
        return Object.freeze(this._data);
    }

    constructor(initData = null) {
        super();
        this._data = initData;
    }

    _setData(data) {
        this._data = data;
        this._notifyObservers(data);
    }
}
