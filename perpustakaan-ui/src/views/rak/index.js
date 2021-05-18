import React, { Component } from 'react';
import { connect } from "react-redux";
import List from './list'
import Form from './form'
import { PAGE_MODE_FORM } from "../../actions/types";
import { backToList } from "../../actions/rak";

class Rak extends Component {
  constructor(props) {
    super(props);

    this.state = {};
    this.props.backToList();
  }

  render() {
    return (
      <>
        {(this.props.pageMode === PAGE_MODE_FORM) ? <Form /> : <List />}
      </>
    );
  }
}

const mapStateToProps = (state) => ({
  data: state.rak.data,
  pageMode: state.rak.pageMode,
  isLoading: state.rak.isLoading
});

export default connect(
  mapStateToProps,
  {
    backToList
  }
)(Rak);