import {
    GET_DATA_PENERBIT,
    SET_DATA_PENERBIT,
    GET_LIST_PENERBIT,
    SET_LIST_PENERBIT,
    SET_ADD_PENERBIT,
    SET_EDIT_PENERBIT,
    SAVE_PENERBIT,
    DELETE_PENERBIT,
    BACK_TO_LIST_PENERBIT,
    API_START,
    API_END,
    API_ERROR
} from "../actions/types";

const initialState = {
    sidebarShow: 'responsive',
}

export default function penerbitReducer(state = initialState, { type, payload, pageMode, dataId, disabled, error, ...rest }) {
    switch (type) {
        case 'set':
            return { ...state, ...rest };
        case SET_DATA_PENERBIT:
        case SET_LIST_PENERBIT:
        case BACK_TO_LIST_PENERBIT:
            return {
                ...state,
                data: payload,
                pageMode: pageMode
            };
        case SET_ADD_PENERBIT:
        case SET_EDIT_PENERBIT:
            return {
                data: payload,
                pageMode: pageMode,
                dataId: dataId || 0,
                disabled: disabled || false
            };
        case API_START:
            {
                switch (payload) {
                    case GET_DATA_PENERBIT:
                    case GET_LIST_PENERBIT:
                    case SAVE_PENERBIT:
                    case DELETE_PENERBIT:
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
                    case GET_DATA_PENERBIT:
                    case GET_LIST_PENERBIT:
                    case SAVE_PENERBIT:
                    case DELETE_PENERBIT:
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