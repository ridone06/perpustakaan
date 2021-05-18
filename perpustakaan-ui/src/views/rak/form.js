import React, { Component } from 'react'
import { connect } from "react-redux";
import {
    CButton,
    CCard,
    CCardBody,
    CCardFooter,
    CCardHeader,
    CCol,
    CForm,
    CFormGroup,
    CFormText,
    CInput,
    CLabel,
    CTextarea,
    CRow
} from '@coreui/react'
import CIcon from '@coreui/icons-react';
import { getByKode, save, backToList } from "../../actions/rak";

class FormRak extends Component {
    constructor(props) {
        super(props);

        this.state = {
            param: {
                Kode: "",
                Lokasi: ""
            }
        }

        this.onClickSave = this.onClickSave.bind(this);
    }

    componentDidMount() {
        if (!this.props.isLoading) {
            this.props.getByKode(this.props.kode || "");
        }
    }

    UNSAFE_componentWillReceiveProps(nextProps) {
        if (nextProps.data) {
            this.setState({
                param: nextProps.data
            });
        }
    }

    onInputChane(key, value) {
        this.state.param[key] = value;
        this.setState({
            param: {
                ...this.state.param
            }
        });
    }

    onClickSave() {
        this.props.save(this.state.param, this.props.kode);
    }

    render() {
        const { isLoading = false, disabled = false, kode } = this.props;
        const { param } = this.state;

        return (
            <>
                <CCard>
                    <CCardHeader>
                        Rak <small> Form</small>
                    </CCardHeader>
                    <CCardBody>
                        <CForm action="" method="post" className="form-horizontal">
                            <CRow>
                                <CCol xs="12" md="6">
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="Kode">Kode</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="text"
                                                id="Kode"
                                                name="Kode"
                                                placeholder="Kode"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.Kode}
                                                onChange={(e) => this.onInputChane("Kode", e.target.value)} />
                                            <CFormText className="help-block">Please enter kode</CFormText>
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="Lokasi">Lokasi</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CTextarea id="Lokasi"
                                                name="Lokasi"
                                                placeholder="Lokasi"
                                                autoComplete="off"
                                                rows="4"
                                                disabled={disabled}
                                                value={param.Lokasi}
                                                onChange={(e) => this.onInputChane("Lokasi", e.target.value)} />
                                            <CFormText className="help-block">Please enter lokasi</CFormText>
                                        </CCol>
                                    </CFormGroup>
                                </CCol>
                            </CRow>
                        </CForm>
                    </CCardBody>
                    <CCardFooter>
                        <CButton hidden={disabled} type="submit" size="sm" color="primary" onClick={this.onClickSave}><CIcon name="cil-save" /> Save</CButton>
                        &nbsp;
                        <CButton type="reset" size="sm" color="warning" onClick={() => {
                            this.props.backToList();
                        }}><CIcon name="cil-chevron-left" /> Back</CButton>
                    </CCardFooter>
                </CCard>
            </>
        );
    }
}

const mapStateToProps = (state) => ({
    data: state.rak.data,
    kode: state.rak.kode,
    disabled: state.rak.disabled,
    isLoading: state.rak.isLoading
});

export default connect(
    mapStateToProps,
    {
        getByKode,
        save,
        backToList
    }
)(FormRak);