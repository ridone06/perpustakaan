import {
    GET_DATA_PENGEMBALIAN,
    SET_DATA_PENGEMBALIAN,
    GET_LIST_PENGEMBALIAN,
    SET_LIST_PENGEMBALIAN,
    SET_ADD_PENGEMBALIAN,
    SET_EDIT_PENGEMBALIAN,
    SAVE_PENGEMBALIAN,
    DELETE_PENGEMBALIAN,
    BACK_TO_LIST_PENGEMBALIAN,
    API_START,
    API_END,
    API_ERROR
} from "../actions/types";

const initialState = {
    sidebarShow: 'responsive',
}

export default function pengembalianReducer(state = initialState, { type, payload, pageMode, dataId, disabled, error, ...rest }) {
    switch (type) {
        case 'set':
            return { ...state, ...rest };
        case SET_DATA_PENGEMBALIAN:
        case SET_LIST_PENGEMBALIAN:
        case BACK_TO_LIST_PENGEMBALIAN:
            return {
                ...state,
                data: payload,
                pageMode: pageMode
            };
        case SET_ADD_PENGEMBALIAN:
        case SET_EDIT_PENGEMBALIAN:
            return {
                data: payload,
                pageMode: pageMode,
                dataId: dataId || 0,
                disabled: disabled || false
            };
        case API_START:
            {
                switch (payload) {
                    case GET_DATA_PENGEMBALIAN:
                    case GET_LIST_PENGEMBALIAN:
                    case SAVE_PENGEMBALIAN:
                    case DELETE_PENGEMBALIAN:
                        return {
                            ...state,
                            isLoading: true
                        };
                    default:
                        return state;
                }
            }
        case API_ERROR:
        case API_END:
            {
                switch (payload) {
                    case GET_DATA_PENGEMBALIAN:
                    case GET_LIST_PENGEMBALIAN:
                    case SAVE_PENGEMBALIAN:
                    case DELETE_PENGEMBALIAN:
                        return {
                            ...state,
                            error: error,
                            isLoading: false
                        };
                    default:
                        return state;
                }
            }
        default:
            return state;
    }
}