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
import { getById, save, backToList } from "../../actions/penerbit";

class FormPenerbit extends Component {
    constructor(props) {
        super(props);

        this.state = {
            param: {
                Nama: "",
                Alamat: "",
                NoTlp: ""
            }
        }

        this.onClickSave = this.onClickSave.bind(this);
    }

    componentDidMount() {
        if (!this.props.isLoading) {
            this.props.getById(this.props.dataId || 0);
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
        this.props.save(this.state.param, this.props.dataId);
    }

    render() {
        const { isLoading = false, disabled = false, dataId } = this.props;
        const { param } = this.state;

        console.log("dataId", dataId);

        return (
            <>
                <CCard>
                    <CCardHeader>
                        Penerbit <small> Form</small>
                    </CCardHeader>
                    <CCardBody>
                        <CForm action="" method="post" className="form-horizontal">
                            <CRow>
                                <CCol xs="12" md="6">
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="Namal">Nama</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="text"
                                                id="Nama"
                                                name="Nama"
                                                placeholder="Nama"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.Nama}
                                                onChange={(e) => this.onInputChane("Nama", e.target.value)} />
                                            <CFormText className="help-block">Please enter nama</CFormText>
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="NoTlp">No. Tlp</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="number"
                                                id="NoTlp"
                                                name="NoTlp"
                                                placeholder="NoTlp"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.NoTlp}
                                                onChange={(e) => this.onInputChane("NoTlp", e.target.value)} />
                                            <CFormText className="help-block">Please enter no. tlp</CFormText>
                                        </CCol>
                                    </CFormGroup>
                                </CCol>
                                <CCol xs="12" md="6">
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="Alamat">Alamat</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CTextarea id="Alamat"
                                                name="Alamat"
                                                placeholder="Alamat"
                                                autoComplete="off"
                                                rows="4"
                                                disabled={disabled}
                                                value={param.Alamat}
                                                onChange={(e) => this.onInputChane("Alamat", e.target.value)} />
                                            <CFormText className="help-block">Please enter alamat</CFormText>
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
    data: state.penerbit.data,
    dataId: state.penerbit.dataId,
    disabled: state.penerbit.disabled,
    isLoading: state.penerbit.isLoading
});

export default connect(
    mapStateToProps,
    {
        getById,
        save,
        backToList
    }
)(FormPenerbit);