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
    CInput,
    CLabel,
    CSelect,
    CTextarea,
    CInvalidFeedback,
    CRow
} from '@coreui/react'
import CIcon from '@coreui/icons-react';
import { getById, save, backToList } from "../../actions/anggota";

class FormPeminjaman extends Component {
    constructor(props) {
        super(props);

        this.state = {
            param: {
                Nama: "",
                JenisKelamin: "",
                Alamat: "",
                Email: "",
                NoTlp: ""
            },
            formValidation: {
                Nama: {
                    invalid: null,
                    message: "Please enter nama"
                },
                JenisKelamin: {
                    invalid: null,
                    message: "Please enter jenis kelamin"
                },
                Alamat: {
                    invalid: null,
                    message: "Please enter alamat"
                },
                Email: {
                    invalid: null,
                    message: "Please enter email"
                },
                NoTlp: {
                    invalid: null,
                    message: "Please enter no tlp"
                }
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
        if (this.state.formValidation[key])
            this.state.formValidation[key].invalid = (this.state.param[key] || "") === "";

        this.setState({
            param: {
                ...this.state.param
            }
        });
    }

    onClickSave() {
        if (this.isValid()) {
            this.props.save(this.state.param, this.props.dataId);
        }
    }

    isValid() {
        this.state.formValidation.Nama.invalid = (this.state.param.Nama || "") === "";
        this.state.formValidation.JenisKelamin.invalid = (this.state.param.JenisKelamin || "") === "";
        this.state.formValidation.Alamat.invalid = (this.state.param.Alamat || "") === "";
        this.state.formValidation.Email.invalid = (this.state.param.Email || "") === "";
        this.state.formValidation.NoTlp.invalid = (this.state.param.NoTlp || "") === "";

        this.setState({
            ...this.state.formValidation
        });

        return !this.state.formValidation.Nama.invalid
            && !this.state.formValidation.JenisKelamin.invalid
            && !this.state.formValidation.Alamat.invalid
            && !this.state.formValidation.Email.invalid
            && !this.state.formValidation.NoTlp.invalid;;
    }

    render() {
        const { isLoading = false, disabled = false, dataId } = this.props;
        const { param, formValidation } = this.state;

        console.log("dataId", dataId);

        return (
            <>
                <CCard>
                    <CCardHeader>
                        Anggota <small> Form</small>
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
                                                onChange={(e) => this.onInputChane("Nama", e.target.value)}
                                                invalid={formValidation.Nama.invalid} />
                                            {
                                                (formValidation.Nama.invalid) ?
                                                    <CInvalidFeedback>{formValidation.Nama.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="JenisKelamin">Jenis Kelamin</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CSelect custom name="JenisKelamin"
                                                id="JenisKelamin"
                                                value={param.JenisKelamin}
                                                disabled={disabled}
                                                invalid={formValidation.JenisKelamin.invalid}
                                                onChange={(e) => this.onInputChane("JenisKelamin", e.target.value)}>
                                                <option value="">Please select</option>
                                                <option value="L">Laki-laki</option>
                                                <option value="P">Perempuan</option>
                                            </CSelect>
                                            {
                                                (formValidation.JenisKelamin.invalid) ?
                                                    <CInvalidFeedback>{formValidation.JenisKelamin.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                    <CFormGroup row>
                                        <CCol md="3">
                                            <CLabel htmlFor="Email">Email</CLabel>
                                        </CCol>
                                        <CCol xs="12" md="9">
                                            <CInput type="text"
                                                id="Email"
                                                name="Email"
                                                placeholder="Email"
                                                autoComplete="off"
                                                disabled={disabled}
                                                value={param.Email}
                                                onChange={(e) => this.onInputChane("Email", e.target.value)}
                                                invalid={formValidation.Email.invalid} />
                                            {
                                                (formValidation.Email.invalid) ?
                                                    <CInvalidFeedback>{formValidation.Email.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
                                </CCol>
                                <CCol xs="12" md="6">
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
                                                onChange={(e) => this.onInputChane("NoTlp", e.target.value)}
                                                invalid={formValidation.NoTlp.invalid} />
                                            {
                                                (formValidation.NoTlp.invalid) ?
                                                    <CInvalidFeedback>{formValidation.NoTlp.message}</CInvalidFeedback> : null
                                            }
                                        </CCol>
                                    </CFormGroup>
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
                                                onChange={(e) => this.onInputChane("Alamat", e.target.value)}
                                                invalid={formValidation.Alamat.invalid} />
                                            {
                                                (formValidation.Alamat.invalid) ?
                                                    <CInvalidFeedback>{formValidation.Alamat.message}</CInvalidFeedback> : null
                                            }
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
    data: state.anggota.data,
    dataId: state.anggota.dataId,
    disabled: state.anggota.disabled,
    isLoading: state.anggota.isLoading
});

export default connect(
    mapStateToProps,
    {
        getById,
        save,
        backToList
    }
)(FormPeminjaman);