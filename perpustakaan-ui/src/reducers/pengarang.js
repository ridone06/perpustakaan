import {
    GET_DATA_PENGARANG,
    SET_DATA_PENGARANG,
    GET_LIST_PENGARANG,
    SET_LIST_PENGARANG,
    SET_ADD_PENGARANG,
    SET_EDIT_PENGARANG,
    SAVE_PENGARANG,
    DELETE_PENGARANG,
    BACK_TO_LIST_PENGARANG,
    API_START,
    API_END,
    API_ERROR
} from "../actions/types";

const initialState = {
    sidebarShow: 'responsive',
}

export default function pengarangReducer(state = initialState, { type, payload, pageMode, dataId, disabled, error, ...rest }) {
    switch (type) {
        case 'set':
            return { ...state, ...rest };
        case SET_DATA_PENGARANG:
        case SET_LIST_PENGARANG:
        case BACK_TO_LIST_PENGARANG:
            return {
                ...state,
                data: payload,
                pageMode: pageMode
            };
        case SET_ADD_PENGARANG:
        case SET_EDIT_PENGARANG:
            return {
                data: payload,
                pageMode: pageMode,
                dataId: dataId || 0,
                disabled: disabled || false
            };
        case API_START:
            {
                switch (payload) {
                    case GET_DATA_PENGARANG:
                    case GET_LIST_PENGARANG:
                    case SAVE_PENGARANG:
                    case DELETE_PENGARANG:
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
                    case GET_DATA_PENGARANG:
                    case GET_LIST_PENGARANG:
                    case SAVE_PENGARANG:
                    case DELETE_PENGARANG:
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